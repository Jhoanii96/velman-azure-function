using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static FastFieldSchemas.Classes.FF_AuthenticateClass;
using static FastFieldSchemas.Classes.FF_FormDefinitionClass;
using static FastFieldSchemas.Classes.FF_FormsClass;
using static FastFieldSchemas.Classes.FF_SubFormDefinitionClass;

namespace VelmanAzureFunction.FastFieldInsertData
{
    public class FastFieldInsertData
    {
        #region GlobalVariables
        private readonly ILogger<FastFieldInsertData> log;

        static string Schema = "dbo";
        static int DaysAgo = -1;
        static string connectionString = string.Empty;
        static SqlConnection cnn;

        static string MainTableName = string.Empty;
        private static readonly List<Headers> ListHeaders = new List<Headers>();
        private static readonly List<ListPossibleChilds> ListChilds = new List<ListPossibleChilds>();

        private static string BaseUrl = string.Empty;
        private static string Username = string.Empty;
        private static string Password = string.Empty;

        #endregion

        public FastFieldInsertData(ILogger<FastFieldInsertData> logger)
        {
            log = logger;
        }

        [Function("FastFieldInsertData")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            // BaseURL
            BaseUrl = "https://api.fastfieldforms.com/services/v3";

            // API Source Credentials
            string FastFieldURL = BaseUrl + "/authenticate";
            string FastFieldUser = Environment.GetEnvironmentVariable("FastFieldUser", EnvironmentVariableTarget.Process) ?? "";
            string FastFieldPassword = Environment.GetEnvironmentVariable("FastFieldPassword", EnvironmentVariableTarget.Process) ?? "";
            string FastFieldAPIKey = Environment.GetEnvironmentVariable("FastFieldAPIKey", EnvironmentVariableTarget.Process) ?? "";

            // Database destination Credentials
            string AzuSQLServerName = Environment.GetEnvironmentVariable("AzuSQLServerName", EnvironmentVariableTarget.Process) ?? "";
            string AzuSQLDatabase = Environment.GetEnvironmentVariable("AzuSQLDatabase", EnvironmentVariableTarget.Process) ?? "";
            string AzuSQLUser = Environment.GetEnvironmentVariable("AzuSQLUser", EnvironmentVariableTarget.Process) ?? "";
            string AzuSQLSecret = Environment.GetEnvironmentVariable("AzuSQLSecret", EnvironmentVariableTarget.Process) ?? "";

            Username = FastFieldUser;
            Password = FastFieldPassword;

            Schema = req.Headers["Schema"].FirstOrDefault() ?? "dbo";
            log.LogInformation(Schema == "dbo" ? "Schema not specified, using default: dbo" : $"Schema specified: {Schema}");

            string form_name = req.Query["FormName"].FirstOrDefault() ?? string.Empty;
            form_name = Uri.UnescapeDataString(form_name);
            if (string.IsNullOrWhiteSpace(form_name))
            {
                log.LogError("El nombre del formulario es requerido");
                return new BadRequestObjectResult("Establezca el nombre de un formulario");
            }
            log.LogInformation($"Form name: {form_name}");

            // GET DAYS_AGO PARAMETER
            var daysAgoParam = req.Headers["DaysAgo"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(daysAgoParam))
            {
                DaysAgo = -1;
                log.LogInformation("Parámetro 'days_ago' no especificado. Se obtendrán todos los registros históricos");
            }
            else
            {
                if (int.TryParse(daysAgoParam, out int parsedDaysAgo) && parsedDaysAgo >= -1)
                {
                    DaysAgo = parsedDaysAgo;
                    log.LogInformation($"Filtrando datos de los últimos {DaysAgo} días");
                }
                else
                {
                    const string errorMessage = "El parámetro 'days_ago' debe ser un número entero válido (≥ -1)";
                    log.LogError("{ErrorMessage}. Valor recibido: {Value}", errorMessage, daysAgoParam);
                    return new BadRequestObjectResult(errorMessage);
                }
            }


            connectionString = $"Server={AzuSQLServerName};Database={AzuSQLDatabase};User Id={AzuSQLUser};Password={AzuSQLSecret};";
            SqlConnection cnn = new(connectionString);

            List<ListForms> listForms = new List<ListForms>();
            log.LogInformation($"FastFieldURL: {FastFieldURL}, FastFieldUser: {FastFieldUser}, FastFieldPassword: *********************, FastFieldAPIKey: *********************");
            try
            {
                ListHeaders.Clear();
                ListHeaders.Add(new Headers { Name = "FastField-API-Key", Value = FastFieldAPIKey });

                //var fastFieldAuthService = new FastFieldHttpService();
                // Authenticate and get session token
                string jsonAuthenticate = MakeRequestAuthenticate(FastFieldURL, FastFieldUser, FastFieldPassword, ListHeaders);
                var authenticateObject = JsonConvert.DeserializeObject<RootAuthenticate>(jsonAuthenticate);

                if (authenticateObject == null || string.IsNullOrEmpty(authenticateObject.sessionToken))
                {
                    log.LogError("Failed to authenticate: Invalid response from authentication service");
                    return new BadRequestObjectResult("Authentication failed");
                }

                string sessionToken = authenticateObject.sessionToken;

                // Get forms list
                string formsUrl = "https://api.fastfieldforms.com/services/v3/forms";
                ListHeaders.Add(new Headers { Name = "X-Gatekeeper-SessionToken", Value = sessionToken });

                string jsonForms = MakeRequestWithBasicAuth(formsUrl, FastFieldUser, FastFieldPassword, ListHeaders);

                if (string.IsNullOrEmpty(jsonForms))
                {
                    log.LogError("Empty response received from forms API");
                    return new BadRequestObjectResult("No forms data received");
                }

                // Parse forms data
                var formsObject = JsonConvert.DeserializeObject<RootForms>($"{{\"Root\": {jsonForms}}}");

                if (formsObject?.Root == null)
                {
                    log.LogError("Failed to deserialize forms data");
                    return new BadRequestObjectResult("Invalid forms data format");
                }

                var allForms = formsObject.Root.ToList();

                // Find the specific form
                string normalizedFormName = form_name?.Replace(" ", "") ?? string.Empty;
                var matchingForm = allForms.FirstOrDefault(x =>
                    x.name?.Replace(" ", "") == normalizedFormName);

                if (matchingForm != null)
                {
                    listForms.Add(new ListForms
                    {
                        Id = matchingForm.id,
                        Name = matchingForm.name,
                        IsSubform = matchingForm.isSubForm
                    });
                }
                else
                {
                    log.LogWarning($"Form '{form_name}' not found. Available forms: {string.Join(", ", allForms.Select(f => f.name))}");
                    return new NotFoundObjectResult($"Form '{form_name}' not found");
                }
            }
            catch (JsonException jsonEx)
            {
                log.LogError(jsonEx, "JSON deserialization error");
                return new BadRequestObjectResult("Invalid data format received from API");
            }
            catch (HttpRequestException httpEx)
            {
                log.LogError(httpEx, "HTTP request error");
                return new StatusCodeResult(StatusCodes.Status502BadGateway);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unexpected error occurred while processing forms");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            FastFieldURL = BaseUrl + "/forms/";
            string query;
            string query_data_first = string.Empty;
            string query_data_middle = string.Empty;
            string query_data_last = string.Empty;
            List<string> ListForName;
            List<ColumnsTable> ListColumnsFromTable;
            List<SubmissionsData> ListSubmissionIDs;
            ListChilds.Clear();
            foreach (var item2 in listForms)
            {
                query = string.Empty;

                if (item2.IsSubform == true)
                {
                    string jsonFormDefinition = MakeRequestWithBasicAuth(FastFieldURL + item2.Id, FastFieldUser, FastFieldPassword, ListHeaders);
                    var FormDefinitionObject = JsonConvert.DeserializeObject<RootSubFormDefinition>(jsonFormDefinition);
                    MainTableName = FormDefinitionObject.formJson.formName;
                    MainTableName = MainTableName.Replace("// Ingeniería de mantenimiento-Dpto. de confiabilidad", "");
                    MainTableName = MainTableName.Replace("// Ingeniería de mantenimiento-Dpto. de Confiabilidad", "");
                    MainTableName = MainTableName.Replace("Pauta de inspección", "");
                    MainTableName = MainTableName.Replace("Dpto. de Confiabilidad", "");
                    MainTableName = MainTableName.Replace("Monitoreo de condiones", "");
                    MainTableName = MainTableName.Replace("Test performance", "");
                    MainTableName = MainTableName.Replace(":", " ").Replace("-", " ").Replace("\\", " ").Replace(".", " ");
                    MainTableName = MainTableName.Replace("/", "-");
                    ListForName = MainTableName.Split(' ').ToList();
                    ListForName = ListForName.Where(x => x != "").ToList();
                    ListForName = ListForName.ConvertAll(d => d.First().ToString().ToUpper() + d.Substring(1).ToLower());
                    MainTableName = ConvertDiacriticsAccent(string.Join("", ListForName));

                }
                else
                {
                    string jsonFormDefinition = MakeRequestWithBasicAuth(FastFieldURL + item2.Id, FastFieldUser, FastFieldPassword, ListHeaders);
                    var FormDefinitionObject = JsonConvert.DeserializeObject<RootFormDefinition>(jsonFormDefinition);
                    MainTableName = FormDefinitionObject.formJson.formName;
                    MainTableName = MainTableName.Replace("// Ingeniería de mantenimiento-Dpto. de confiabilidad", "");
                    MainTableName = MainTableName.Replace("// Ingeniería de mantenimiento-Dpto. de Confiabilidad", "");
                    MainTableName = MainTableName.Replace("Pauta de inspección", "");
                    MainTableName = MainTableName.Replace("Dpto. de Confiabilidad", "");
                    MainTableName = MainTableName.Replace("Monitoreo de condiones", "");
                    MainTableName = MainTableName.Replace("Test performance", "");
                    MainTableName = MainTableName.Replace(":", " ").Replace("-", " ").Replace("\\", " ").Replace(".", " ");
                    MainTableName = MainTableName.Replace("/", "-");
                    ListForName = MainTableName.Split(' ').ToList();
                    ListForName = ListForName.Where(x => x != "").ToList();
                    ListForName = ListForName.ConvertAll(d => d.First().ToString().ToUpper() + d.Substring(1).ToLower());
                    MainTableName = ConvertDiacriticsAccent(string.Join("", ListForName));
                }
                // Get Columns 
                ListColumnsFromTable = GetColumnsTable(MainTableName);
                if (ListColumnsFromTable == null)
                {
                    log.LogError($"Error: no se encontró ninguna columna, debido que las tablas no existen.");
                    return new OkObjectResult("Error: no se encontró ninguna columna, debido que las tablas no existen.");
                }
                log.LogInformation($"Table Database Name: {MainTableName}");
                // Get Submissions
                ListSubmissionIDs = GetSQLDataSubmission(MainTableName);
                if (ListSubmissionIDs.Count == 0)
                {
                    log.LogInformation($"No se encontraron registros de submissions desde: '{DateTime.Now.AddDays(-1 * DaysAgo):yyyy-MM-dd} 00:00:00'");
                    return new OkObjectResult($"No se encontraron registros de submissions desde: '{DateTime.Now.AddDays(-1 * DaysAgo):yyyy-MM-dd} 00:00:00'");
                }
                int totalsubmission = ListSubmissionIDs.Count;
                int countingsubmission = 0;
                bool master_table = true;
                bool IsError404;
                dynamic itemJsonDynamic;
                List<string> table_name = ListColumnsFromTable.Select(i => i.TableName).Distinct().ToList();

                foreach (var itemSubmission in ListSubmissionIDs)
                {
                    countingsubmission += 1;
                    List<QueryTable> mapListQueryTable = new List<QueryTable>();
                    IsError404 = false;

                    // ITERACIONNN
                    foreach (var itemtables in table_name)
                    {
                        if (MainTableName + "_1" == itemtables || MainTableName + "_2" == itemtables || MainTableName + "_3" == itemtables)
                        {
                            master_table = true;
                        }
                        else
                        {
                            master_table = MainTableName == itemtables;
                        }

                        List<string> columnstable = ListColumnsFromTable.Where(x => x.TableName == itemtables).Select(y => y.ColumnName).OrderBy(y => y).ToList();
                        if (master_table)
                        {
                            columnstable = columnstable.Where(x => x != "ID").OrderBy(x => x).ToList();
                        }
                        else
                        {
                            columnstable = columnstable.Where(x => x != "Id").OrderBy(x => x).ToList();
                        }

                        query_data_first = string.Empty;
                        query_data_middle = string.Empty;
                        query_data_last = string.Empty;

                        // -------------------------- CREATING QUERY INSERT MERGE TABLE STATEMENT -------------------------- //
                        var newlistupdate = columnstable.Where(x => x != "SUBMISSIONID").OrderBy(x => x).ToList();
                        var last_columns = newlistupdate.Last();

                        query_data_first += $"MERGE [{Schema}].[{itemtables}] AS Target {Environment.NewLine}";
                        query_data_first += $"USING ( SELECT DISTINCT [{string.Join("], [", columnstable)}]   {Environment.NewLine}";
                        query_data_first += $"FROM ( VALUES ";
                        query_data_first += $"{Environment.NewLine}";
                        query_data_middle += Environment.NewLine;

                        if (master_table)
                        {

                            query_data_last += $") AS Source ([{string.Join("], [", columnstable)}]) {Environment.NewLine}";
                            query_data_last += $") AS Source {Environment.NewLine}";
                            query_data_last += $" ON Source.[SUBMISSIONID] = Target.[SUBMISSIONID]";
                            query_data_last += $"{Environment.NewLine}";
                            query_data_last += $"WHEN MATCHED THEN {Environment.NewLine} UPDATE SET ";
                            foreach (var columns in newlistupdate)
                            {
                                if (last_columns == columns)
                                {
                                    query_data_last += $" Target.[{columns}] = Source.[{columns}] ";
                                }
                                else
                                {
                                    query_data_last += $" Target.[{columns}] = Source.[{columns}], ";
                                }
                            }
                            query_data_last += $"{Environment.NewLine}";
                            query_data_last += $"WHEN NOT MATCHED BY Target THEN {Environment.NewLine} " +
                                $"INSERT ([{string.Join("], [", columnstable)}])";
                            query_data_last += $"{Environment.NewLine}";
                            query_data_last += $" VALUES (Source.[{string.Join("], Source.[", columnstable)}]); ";

                            List<string> columnslower = columnstable.ConvertAll(d => d.ToLower()).OrderBy(x => x).ToList();
                            if (!mapListQueryTable.Any(x => x.TableName == itemtables))
                            {
                                mapListQueryTable.Add(new QueryTable { TableName = itemtables, FirstQuery = query_data_first, MiddleQuery = query_data_middle, LastQuery = query_data_last, Columns = columnslower, MasterTable = master_table });
                            }

                        }
                        else
                        {
                            query_data_last += $") AS Source ([{string.Join("], [", columnstable)}]) {Environment.NewLine}";
                            query_data_last += $") AS Source {Environment.NewLine}";
                            if (newlistupdate.Any(x => x.EndsWith("_photo")))
                            {
                                query_data_last += $" ON Source.[submissionId] = Target.[submissionId] and Source.[{newlistupdate.Find(x => x.EndsWith("_photo"))}] = Target.[{newlistupdate.Find(x => x.EndsWith("_photo"))}]";
                            }
                            else
                            {
                                query_data_last += $" ON Source.[submissionId] = Target.[submissionId] and Source.[{newlistupdate.Where(x => x != "submissionId").ToList()[0]}] = Target.[{newlistupdate.Where(x => x != "submissionId").ToList()[0]}]";
                            }
                            query_data_last += $"{Environment.NewLine}";
                            query_data_last += $"WHEN MATCHED THEN {Environment.NewLine} UPDATE SET ";
                            foreach (var columns in newlistupdate)
                            {
                                if (last_columns == columns)
                                {
                                    query_data_last += $" Target.[{columns}] = Source.[{columns}] ";
                                }
                                else
                                {
                                    query_data_last += $" Target.[{columns}] = Source.[{columns}], ";
                                }
                            }
                            query_data_last += $"{Environment.NewLine}";
                            query_data_last += $"WHEN NOT MATCHED BY Target THEN {Environment.NewLine} " +
                                $"INSERT ([{string.Join("], [", columnstable)}])";
                            query_data_last += $"{Environment.NewLine}";
                            query_data_last += $" VALUES (Source.[{string.Join("], Source.[", columnstable)}]); ";

                            List<string> columnslower = columnstable.ConvertAll(d => d.ToLower()).OrderBy(x => x).ToList();
                            if (!mapListQueryTable.Any(x => x.TableName == itemtables))
                            {
                                mapListQueryTable.Add(new QueryTable { TableName = itemtables, FirstQuery = query_data_first, MiddleQuery = query_data_middle, LastQuery = query_data_last, Columns = columnslower, MasterTable = master_table });
                            }

                        }

                    }

                    Thread.Sleep(1000);
                    itemJsonDynamic = MakeRequestGetFormResult(itemSubmission.SubmissionId, ListHeaders);
                    if (itemJsonDynamic == null)
                    {
                        log.LogError($"{itemSubmission.SubmissionId} error retrieve null item");
                        continue;
                    }
                    if (itemJsonDynamic.ContainsKey("Code"))
                    {
                        if (itemJsonDynamic["Code"] == 404)
                        {
                            log.LogError($"SubmissionId {itemSubmission.SubmissionId}: {itemJsonDynamic["message"]}");
                            IsError404 = true;
                        }
                        continue;
                    }
                    List<string> valuesToMatch = OnlyOneList(mapListQueryTable);
                    List<ColumnsValues> valuesfromjson = new List<ColumnsValues>();
                    foreach (JToken itemJson in itemJsonDynamic)
                    {
                        var childproperty = itemJson as JProperty;
                        string valueColumn = string.Empty;

                        if (childproperty.Name.ToLower() == "formmetadata")
                        {
                            string startDateTime = childproperty.Value["startFormTimeStamp"].ToString();
                            string endDateTime = childproperty.Value["endFormTimeStamp"].ToString();
                            valuesfromjson.Add(new ColumnsValues { TableName = MainTableName, Column = "startformtimestamp", Value = startDateTime });
                            valuesfromjson.Add(new ColumnsValues { TableName = MainTableName, Column = "endformtimestamp", Value = endDateTime });
                        }

                        if (valuesToMatch.FirstOrDefault(x => x.Contains(childproperty.Name.ToLower())) != null && childproperty != null)
                        {
                            int SerieRow = 0;
                            int SerieColumn = 0;

                            if (childproperty.Value.HasValues == false && childproperty.Value.Type.ToString() != "Array" && childproperty.Value.Type.ToString() != "Object")
                            {
                                SerieRow += 1;
                                SerieColumn = 1;
                                valuesfromjson.Add(new ColumnsValues { TableName = MainTableName + "_" + childproperty.Name.ToString().ToLower(), Column = childproperty.Name.ToString().ToLower(), Value = childproperty.Value.ToString(), NumRow = SerieRow, NumColumn = SerieColumn });
                            }
                            else
                            {
                                foreach (var itemArray in childproperty.Value)
                                {
                                    SerieRow += 1;
                                    SerieColumn = 1;

                                    if (itemArray.Type.ToString() == "String")
                                    {
                                        valuesfromjson.Add(new ColumnsValues { TableName = MainTableName + "_" + childproperty.Name.ToString().ToLower(), Column = childproperty.Name.ToString().ToLower(), Value = itemArray.ToString(), NumRow = SerieRow, NumColumn = SerieColumn });
                                    }
                                    else if (itemArray.Type.ToString() == "Object")
                                    {
                                        foreach (var itemobject in itemArray)
                                        {
                                            var childproperty2 = itemobject as JProperty;
                                            valuesfromjson.Add(new ColumnsValues { TableName = MainTableName + "_" + childproperty.Name.ToString().ToLower(), Column = childproperty.Name.ToString().ToLower() + "_" + childproperty2.Name.ToString().ToLower(), Value = childproperty2.Value.ToString(), NumRow = SerieRow, NumColumn = SerieColumn });
                                            SerieColumn += 1;
                                        }
                                    }
                                    else
                                    {
                                        var childproperty2 = itemArray as JProperty;
                                        valuesfromjson.Add(new ColumnsValues { TableName = MainTableName + "_" + childproperty.Name.ToString().ToLower(), Column = childproperty.Name.ToString().ToLower(), Value = childproperty2.Value.ToString(), NumRow = SerieRow, NumColumn = SerieColumn });
                                    }
                                }
                            }

                        }
                    }

                    foreach (var itemList in mapListQueryTable)
                    {
                        if (itemList.MasterTable == true)
                        {
                            itemList.MiddleQuery += "(";
                        }
                    }

                    string values_column = string.Empty;
                    string last_column = string.Empty;
                    valuesfromjson = valuesfromjson.OrderBy(c => c.NumRow).ThenBy(c => c.NumColumn).ToList();
                    foreach (var itemList in mapListQueryTable)
                    {
                        last_column = itemList.Columns.Last();
                        if (itemList.MasterTable == true)
                        {
                            var itemListColumns = itemList.Columns.OrderBy(x => x);
                            foreach (var itemColumn in itemListColumns)
                            {
                                if (valuesfromjson.Any(b => b.Column == itemColumn))
                                {
                                    values_column = valuesfromjson.Find(b => b.Column == itemColumn).Value;

                                    try
                                    {
                                        if (values_column.EndsWith("jpg") || values_column.EndsWith("JPG") || values_column.EndsWith("png") || values_column.EndsWith("PNG") || values_column.EndsWith("jpeg") || values_column.EndsWith("JPEG"))
                                        {
                                            values_column = "https://manage.fastfieldforms.com/Reports/ReportAsset?assetKey=" + values_column;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    if (values_column != "" || values_column != null)
                                    {
                                        DateTime temp;
                                        decimal tmpDec;
                                        int tmpInt;

                                        var isDecimal = decimal.TryParse(values_column.ToString(), out tmpDec);
                                        var isInteger = int.TryParse(values_column.ToString(), out tmpInt);

                                        if (!isInteger && !isDecimal && DateTime.TryParse(values_column.ToString(), out temp))
                                        {
                                            string date_time = Convert.ToDateTime(values_column).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                            values_column = date_time;
                                        }
                                    }

                                }
                                else
                                {
                                    values_column = "NULL";
                                }

                                if (values_column == "NULL" && last_column == itemColumn)
                                {
                                    itemList.MiddleQuery += $" {values_column.Replace("'", "''")}),";
                                }
                                else if (values_column != "NULL" && last_column == itemColumn)
                                {
                                    itemList.MiddleQuery += $" '{values_column.Replace("'", "''")}'),";
                                }
                                else if (values_column == "NULL" && last_column != itemColumn)
                                {
                                    itemList.MiddleQuery += $" {values_column.Replace("'", "''")},";
                                }
                                else
                                {
                                    itemList.MiddleQuery += $" '{values_column.Replace("'", "''")}',";
                                }
                            }
                            itemList.MiddleQuery += Environment.NewLine;
                        }
                        else
                        {
                            if (valuesfromjson.Any(b => b.TableName == itemList.TableName))
                            {
                                var counting_row = valuesfromjson.Where(a => a.TableName == itemList.TableName).ToList().Max(b => b.NumRow);
                                for (int i = 1; i <= counting_row; i++)
                                {
                                    var list_row = valuesfromjson.Where(b => b.TableName == itemList.TableName && b.NumRow == i).ToList();
                                    //string last_column_in_row = itemList.Columns.Last();
                                    var itemListColumns = itemList.Columns.OrderBy(x => x);
                                    var tableparentname = itemList.TableName;
                                    itemList.MiddleQuery += $"(";

                                    foreach (var itemColumn in itemListColumns)
                                    {

                                        if (valuesfromjson.Any(b => b.TableName == tableparentname))
                                        {
                                            var itemsfromjsoncolumns = valuesfromjson.Where(b => b.TableName == tableparentname).ToList();
                                            if (list_row.Any(x => x.Column == itemColumn && x.NumRow == i))
                                            {
                                                values_column = list_row.Find(x => x.Column == itemColumn && x.NumRow == i).Value;
                                            }
                                            else
                                            {
                                                if (valuesfromjson.Any(x => x.Column == itemColumn))
                                                {
                                                    values_column = valuesfromjson.Find(x => x.Column == itemColumn).Value;
                                                }
                                                else
                                                {
                                                    values_column = "NULL";
                                                }
                                            }


                                            try
                                            {
                                                if (values_column.EndsWith("jpg") || values_column.EndsWith("JPG") || values_column.EndsWith("png") || values_column.EndsWith("PNG") || values_column.EndsWith("jpeg") || values_column.EndsWith("JPEG"))
                                                {
                                                    values_column = "https://manage.fastfieldforms.com/Reports/ReportAsset?assetKey=" + values_column;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }

                                            if (values_column != "" || values_column != null)
                                            {
                                                DateTime temp;
                                                decimal tmpDec;
                                                int tmpInt;

                                                var isDecimal = decimal.TryParse(values_column.ToString(), out tmpDec);
                                                var isInteger = int.TryParse(values_column.ToString(), out tmpInt);

                                                if (!isInteger && !isDecimal && DateTime.TryParse(values_column.ToString(), out temp))
                                                {
                                                    string date_time = Convert.ToDateTime(values_column).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                                    values_column = date_time;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            values_column = "NULL";
                                        }

                                        if (values_column == "NULL" && last_column == itemColumn)
                                        {
                                            itemList.MiddleQuery += $" {values_column.Replace("'", "''")}),";
                                        }
                                        else if (values_column != "NULL" && last_column == itemColumn)
                                        {
                                            itemList.MiddleQuery += $" '{values_column.Replace("'", "''")}'),";
                                        }
                                        else if (values_column == "NULL" && last_column != itemColumn)
                                        {
                                            itemList.MiddleQuery += $" {values_column.Replace("'", "''")},";
                                        }
                                        else
                                        {
                                            itemList.MiddleQuery += $" '{values_column.Replace("'", "''")}',";
                                        }

                                    }

                                    itemList.MiddleQuery += Environment.NewLine;
                                }

                            }
                            else
                            {
                                itemList.MiddleQuery = string.Empty;
                            }

                        }

                    }

                    string all_query = string.Empty;
                    int index1 = 0;

                    foreach (var itemList in mapListQueryTable)
                    {
                        if (!string.IsNullOrEmpty(itemList.MiddleQuery))
                        {
                            index1 = itemList.MiddleQuery.LastIndexOf(',');
                            itemList.MiddleQuery = itemList.MiddleQuery.Remove(index1, 1);

                            all_query += itemList.FirstQuery +
                            itemList.MiddleQuery + Environment.NewLine +
                            itemList.LastQuery + Environment.NewLine + Environment.NewLine;
                        }
                    }

                    if (itemJsonDynamic != null && IsError404 == false)
                    {
                        log.LogInformation($"SubmissionId {itemSubmission.SubmissionId}: {countingsubmission} of {totalsubmission} ");
                        WriteSQLDataFromResult(all_query, log);
                    }

                }

            }

            log.LogInformation($"Todas las tablas ejecutadas exitósamente.");
            Thread.Sleep(1000);

            log.LogInformation($"Function executed successfully.");
            Thread.Sleep(500);
            return new OkObjectResult($"Function executed successfully.");

        }



        /* ------------------------------------------- LOCAL FUNCTIONS ---------------------------------------------- */
            #region Local Function

        public static List<string> OnlyOneList(List<QueryTable> mapListQueryTable)
        {
            List<string> listone = new List<string>();
            foreach (var item1 in mapListQueryTable)
            {
                foreach (var item2 in item1.Columns)
                {
                    listone.Add(item2);
                }
            }
            return listone;
        }

        public static List<ColumnsTable> GetColumnsTable(string table_name)
        {
            SqlCommand command;
            cnn = new SqlConnection(connectionString);
            string query = $"SELECT TABLE_NAME, " +
                $"COLUMN_NAME, " +
                $"ROW_NUMBER() OVER (PARTITION BY TABLE_NAME ORDER BY TABLE_NAME, COLUMN_NAME ASC) AS [RowNumber] FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE TABLE_NAME like '%{table_name}%' " +
                $"AND TABLE_SCHEMA = '{Schema}' ; ";
            try
            {
                if (cnn.State == ConnectionState.Open)
                {
                    Thread.Sleep(500);
                    cnn.Close();
                    Thread.Sleep(500);
                }
                cnn.Open();
                command = new SqlCommand(query, cnn)
                {
                    CommandTimeout = 3600
                };
                List<ColumnsTable> results = new List<ColumnsTable>();
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ColumnsTable newItem = new ColumnsTable
                        {
                            TableName = dr.GetString(0),
                            ColumnName = dr.GetString(1),
                            RowNumber = Convert.ToInt32(dr["RowNumber"])
                        };
                        results.Add(newItem);
                    }
                }
                Thread.Sleep(1000);

                Console.WriteLine("The API Forms from FastField was send to DataBase.");

                cnn.Close();
                Thread.Sleep(10);
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }
        }

        public static dynamic MakeRequestGetFormResult(string SubmissionIDs, List<Headers> headers)
        {
            var client = new HttpClient();
            string messages = string.Empty;
            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Username + ":" + Password));
            // Request headers
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
            if (headers.Count > 0)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Name, item.Value);
                }
            }

            var uri = BaseUrl + "/formresults/submission/" + SubmissionIDs;

            HttpResponseMessage response;
            string json_content;
            JObject OdataJson;
            try
            {
                response = Task.Run(async () => await client.GetAsync(uri)).Result;
                json_content = Task.Run(async () => await response.Content.ReadAsStringAsync()).Result;
                // Convert Object to JSON 
                OdataJson = JObject.Parse(json_content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }

            if (OdataJson.ContainsKey("accountId")) { OdataJson.Property("accountId").Remove(); }
            if (OdataJson.ContainsKey("emailRecipientsOnSubmit")) { OdataJson.Property("emailRecipientsOnSubmit").Remove(); }
            if (OdataJson.ContainsKey("resubmit")) { OdataJson.Property("resubmit").Remove(); }
            if (OdataJson.ContainsKey("userId")) { OdataJson.Property("userId").Remove(); }
            if (OdataJson.ContainsKey("alerts")) { OdataJson.Property("alerts").Remove(); }
            if (OdataJson.ContainsKey("workflowData")) { OdataJson.Property("workflowData").Remove(); }
            if (OdataJson.ContainsKey("submitId")) { OdataJson.Property("submitId").Remove(); }

            if (OdataJson.ContainsKey("isWebForm")) { OdataJson.Property("isWebForm").Remove(); }
            if (OdataJson.ContainsKey("creationTimeStamp")) { OdataJson.Property("creationTimeStamp").Remove(); }
            if (OdataJson.ContainsKey("submissionTimeStamp")) { OdataJson.Property("submissionTimeStamp").Remove(); }
            if (OdataJson.ContainsKey("tokenId")) { OdataJson.Property("tokenId").Remove(); }
            if (OdataJson.ContainsKey("stage_var")) { OdataJson.Property("stage_var").Remove(); }
            if (OdataJson.ContainsKey("CURRENT_USER_VAR")) { OdataJson.Property("CURRENT_USER_VAR").Remove(); }

            dynamic ObjectValue = JsonConvert.DeserializeObject(OdataJson.ToString());


            return ObjectValue;

        }

        private static string MakeRequestAuthenticate(string url, string applicationId, string password, List<Headers> headers)
        {
            var client = new HttpClient();

            // Request headers
            string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                               .GetBytes(applicationId + ":" + password));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
            if (headers.Count > 0)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Name, item.Value);
                }
            }

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = Task.Run(async () => await client.PostAsync(url, content)).Result.Content.ReadAsStringAsync();
                return result.Result;
            }
        }

        private static string MakeRequestWithBasicAuth(string url, string applicationId, string password, List<Headers> headers)
        {
            string responseData = null;

            try
            {
                if (url != string.Empty)
                {
                    var webRequest = WebRequest.Create(url);
                    string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                               .GetBytes(applicationId + ":" + password));
                    webRequest.PreAuthenticate = true;
                    webRequest.Headers.Add("Authorization", "Basic " + encoded);
                    if (headers.Count > 0)
                    {
                        foreach (var item in headers)
                        {
                            webRequest.Headers[item.Name] = item.Value;
                        }
                    }

                    //Get the response from the url we provided.
                    using (var response = webRequest.GetResponse() as HttpWebResponse)
                    {
                        /*
                         * Create a reader to access the xml data.
                         */
                        if (response != null)
                        {
                            var responseStream = response.GetResponseStream();
                            if (responseStream != null)
                            {
                                using (var reader = new StreamReader(responseStream))
                                {
                                    responseData = reader.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null && ((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    /*
                     * 404 or not found is a normally occuring exception that indicates the end of queued messages.
                     * That is when traversing a queue eventually a request will be formulated that attempts to retrieve
                     * data that does not exist.  The 404 return code indicates no more data.
                     */
                }
                else
                {
                    var errorString = new StringBuilder();
                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (e.Response != null)
                        {
                            errorString.AppendFormat("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                            errorString.AppendFormat(" - Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                        }
                    }
                    else
                    {
                        errorString.AppendFormat("Error: {0}", e.Status);
                    }

                    Console.WriteLine(errorString.ToString());
                    //log.LogInformation(errorString.ToString());
                }
            }
            return responseData;
        }

        private static string ConvertDiacriticsAccent(string accentedStr)
        {
            var normalizedString = accentedStr.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static List<SubmissionsData> GetSQLDataSubmission(string formName)
        {

            List<SubmissionsData> SubmissionIds = new List<SubmissionsData>();

            cnn = new SqlConnection(connectionString);
            string QuerySource;
            if (DaysAgo <= -1)
            {
                QuerySource = $"select SubmissionId, FormName, SubmittedOn from [dbo].[{formName}_SubmissionId]";
            }
            else if (DaysAgo == 0)
            {
                QuerySource = $"select SubmissionId, FormName, SubmittedOn from [dbo].[{formName}_SubmissionId] where SubmittedOn >= '{DateTime.Now:yyyy-MM-dd} 00:00:00'";
            }
            else
            {
                QuerySource = $"select SubmissionId, FormName, SubmittedOn from [dbo].[{formName}_SubmissionId] where SubmittedOn >= '{DateTime.Now.AddDays(-1 * DaysAgo):yyyy-MM-dd} 00:00:00'";
            }

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                if (cnn.State == ConnectionState.Open)
                {
                    Thread.Sleep(500);
                    cnn.Close();
                    Thread.Sleep(500);
                }
                SqlCommand command = new SqlCommand(QuerySource, connection)
                {
                    CommandTimeout = 3600
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SubmissionIds.Add(new SubmissionsData { SubmissionId = reader.GetString(0), FormName = reader.GetString(1), SubmittedOn = reader.GetDateTime(2) });
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    Thread.Sleep(500);
                    connection.Close();
                }
                Console.WriteLine("Error: " + ex.Message);
            }

            return SubmissionIds;

        }

        public static void WriteSQLDataFromResult(string query, ILogger log)
        {
            SqlCommand command;
            SqlDataReader reader;
            int num_rows;

            cnn = new SqlConnection(connectionString);

            try
            {
                if (cnn.State == ConnectionState.Open)
                {
                    Thread.Sleep(500);
                    cnn.Close();
                    Thread.Sleep(500);
                }
                cnn.Open();
                command = new SqlCommand(query, cnn)
                {
                    CommandTimeout = 3600
                };
                reader = command.ExecuteReader();
                num_rows = reader.RecordsAffected;
                //Console.WriteLine($"Ejecución exitosa: ({num_rows}) Insertados o Modificados.");
                log.LogInformation($"Ejecución exitosa: ({num_rows}) Insertados o Modificados.");
                cnn.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"{ex.Message}");
                log.LogError($"{ex.Message}");
            }
        }


        #endregion

        public class Headers
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class ListForms
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool? IsSubform { get; set; }
        }

        public class ListPossibleChilds
        {
            public string ParentTableName { get; set; }
            public string FieldKey { get; set; }
            public string Type { get; set; }
            public bool? GeoLocation { get; set; }
        }

        public class ColumnsTable
        {
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public int RowNumber { get; set; }
        }
        public class SubmissionsData
        {
            public string SubmissionId { get; set; }
            public string FormName { get; set; }
            public DateTime SubmittedOn { get; set; }
        }

        public class QueryTable
        {
            public string TableName { get; set; }
            public string SimpleTableName { get; set; }
            public string FirstQuery { get; set; }
            public string MiddleQuery { get; set; }
            public string LastQuery { get; set; }
            public List<string> Columns { get; set; }
            public bool MasterTable { get; set; }
        }

        public class ColumnsValues
        {
            public string TableName { get; set; }
            public string Column { get; set; }
            public string Value { get; set; }
            public int NumRow { get; set; }
            public int NumColumn { get; set; }
        }


    }
}
