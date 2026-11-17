using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static FastFieldSchemas.Classes.FF_AuthenticateClass;
using static FastFieldSchemas.Classes.FF_FormDefinitionClass;
using static FastFieldSchemas.Classes.FF_FormsClass;
using static FastFieldSchemas.Classes.FF_SubFormDefinitionClass;

namespace VisionLinkReader
{
    public class FastFieldGetSchema
    {
        #region GlobalVariables

        private readonly ILogger<FastFieldGetSchema> log;
        private static string AzuSQLServerName = string.Empty;
        private static string AzuSQLDatabase = string.Empty;
        private static string AzuSQLUser = string.Empty;
        private static string AzuSQLSecret = string.Empty;

        private static string FastFieldAPIKey = string.Empty;
        private static string FastFieldURL = string.Empty;
        private static string FastFieldUser = string.Empty;
        private static string FastFieldPassword = string.Empty;

        static string MainTableName = string.Empty;
        private static readonly List<Headers> ListHeaders = new List<Headers>();
        private static readonly List<ListPossibleChilds> ListChilds = new List<ListPossibleChilds>();
        private static readonly List<ListColumns> ListMainColumns = new List<ListColumns>();

        #endregion

        public FastFieldGetSchema(ILogger<FastFieldGetSchema> logger)
        {
            log = logger;
        }

        [Function("FastFieldGetSchema")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {

            // API Source Credentials
            FastFieldURL = "https://api.fastfieldforms.com/services/v3/authenticate";
            FastFieldUser = Environment.GetEnvironmentVariable("FastFieldUser", EnvironmentVariableTarget.Process) ?? "";
            FastFieldPassword = Environment.GetEnvironmentVariable("FastFieldPassword", EnvironmentVariableTarget.Process) ?? "";
            FastFieldAPIKey = Environment.GetEnvironmentVariable("FastFieldAPIKey", EnvironmentVariableTarget.Process) ?? "";

            // Database destination Credentials
            AzuSQLServerName = Environment.GetEnvironmentVariable("AzuSQLServerName", EnvironmentVariableTarget.Process) ?? "";
            AzuSQLDatabase = Environment.GetEnvironmentVariable("AzuSQLDatabase", EnvironmentVariableTarget.Process) ?? "";
            AzuSQLUser = Environment.GetEnvironmentVariable("AzuSQLUser", EnvironmentVariableTarget.Process) ?? "";
            AzuSQLSecret = Environment.GetEnvironmentVariable("AzuSQLSecret", EnvironmentVariableTarget.Process) ?? "";

            string? form_name = string.Empty;

            // GET SCHEMA NAME ON THE PARAMETER   
            var schema = req.Headers["schema"].FirstOrDefault() ?? "dbo";
            log.LogInformation(schema == "dbo" ? "Schema not specified, using default: dbo" : $"Schema specified: {schema}");

            form_name = req.Query["form_name"];
            if (string.IsNullOrWhiteSpace(form_name))
            {
                log.LogError("El nombre del formulario es requerido");
                return new BadRequestObjectResult("Establezca el nombre de un formulario");
            }
            form_name = Uri.UnescapeDataString(form_name);

            log.LogInformation($"Nombre del formulario: {form_name}");

            SqlCommand command;
            string connectionString = $"Server={AzuSQLServerName};Database={AzuSQLDatabase};User Id={AzuSQLUser};Password={AzuSQLSecret};";
            SqlConnection cnn = new(connectionString);

            List<ListForms> listForms = new List<ListForms>();
            int counting_inputs;

            ListHeaders.Clear();
            ListHeaders.Add(new Headers { Name = "FastField-API-Key", Value = FastFieldAPIKey });
            string jsonAuthenticate = MakeRequestAuthenticate(FastFieldURL, FastFieldUser, FastFieldPassword, ListHeaders);
            var AuthenticateObject = JsonConvert.DeserializeObject<RootAuthenticate>(jsonAuthenticate);
            string SessionToken = AuthenticateObject.sessionToken;

            FastFieldURL = "https://api.fastfieldforms.com/services/v3/forms";
            ListHeaders.Add(new Headers { Name = "X-Gatekeeper-SessionToken", Value = SessionToken });
            var jsonForms = MakeRequestWithBasicAuth(FastFieldURL, FastFieldUser, FastFieldPassword, ListHeaders);
            jsonForms = "{\"Root\": " + jsonForms + "}";
            var FormsObject = JsonConvert.DeserializeObject<RootForms>(jsonForms);
            var ListForms = FormsObject.Root.ToList();

            if (ListForms.Any(x => x.name.Trim() == form_name.Trim()))
            {
                var formId = ListForms.Find(x => x.name.Trim() == form_name.Trim()).id;
                var formName = ListForms.Find(x => x.name.Trim() == form_name.Trim()).name;
                var formIsSubForm = ListForms.Find(x => x.name.Trim() == form_name.Trim()).isSubForm;
                listForms.Add(new ListForms { Id = formId, Name = formName, IsSubform = formIsSubForm });
            }
            else
            {
                log.LogError($"El formulario no fue encontrado.");
                return new OkObjectResult("El formulario no fue encontrado.");
            }

            FastFieldURL = "https://api.fastfieldforms.com/services/v3/forms/";
            string type = null;
            string query;
            List<String> ListForName;
            ListChilds.Clear();
            ListMainColumns.Clear();
            foreach (var item1 in listForms)
            {
                counting_inputs = 0;
                if (item1.IsSubform == true)
                {
                    string jsonFormDefinition = MakeRequestWithBasicAuth(FastFieldURL + item1.Id, FastFieldUser, FastFieldPassword, ListHeaders);
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
                    ListForName = MainTableName.Split(" ").ToList();
                    ListForName = ListForName.Where(x => x != "").ToList();
                    ListForName = ListForName.ConvertAll(d => d.First().ToString().ToUpper() + d.Substring(1).ToLower());
                    MainTableName = ConvertDiacriticsAccent(String.Join("", ListForName));

                    foreach (var itemSection in FormDefinitionObject.formJson.section)
                    {
                        foreach (var itemField in itemSection.field)
                        {
                            if (itemField.fieldType == "TextLabel" && itemField.value != null)
                            {
                                type = "[nvarchar](4000) NULL";
                                ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = (itemField.fieldKey).ToUpper(), Type = type });
                                counting_inputs += 1;
                            }

                            if (itemField.fieldType != "MultiPhotoPicker" && itemField.fieldType != "ListPicker" && itemField.fieldType != "TextLabel")
                            {
                                if (itemField.fieldValueType == "String" && itemField.fieldType == "SingleLineEntryAlpha")
                                {
                                    if (itemField.enableMinMaxValues)
                                    {
                                        type = $"[nvarchar]({itemField.maxLength}) NULL";
                                    }
                                    else
                                    {
                                        type = "[nvarchar](4000) NULL";
                                    }
                                }
                                else if (itemField.fieldValueType == "String" && (itemField.fieldType == "MultiLineEntry" || itemField.fieldType == "VideoPicker" || itemField.fieldType == "AudioPicker"))
                                {
                                    type = "[nvarchar](4000) NULL";
                                }
                                else if (itemField.fieldValueType == "Integer" && (itemField.fieldType == "SingleLineEntryNumeric" || itemField.fieldType == "FormattedInput" || itemField.fieldType == "ComputedLabel"))
                                {
                                    type = "[nvarchar](4000) NULL";
                                }
                                else if (itemField.fieldValueType == "Double" && itemField.fieldType == "SingleLineEntryNumeric")
                                {
                                    type = "[nvarchar](4000) NULL";
                                }
                                else if (itemField.fieldValueType == "Uncomparable" && (itemField.fieldType == "PhotoPicker" || itemField.fieldType == "SignatureCapture" || itemField.fieldType == "InlinePhoto"))
                                {
                                    type = "[nvarchar](4000) NULL";
                                }
                                else if (itemField.fieldValueType == "DateOnly" && (itemField.fieldType == "DatePicker"))
                                {
                                    type = "[datetime] NULL";
                                }
                                else if (itemField.fieldValueType == "TimeOnly" && (itemField.fieldType == "DatePicker"))
                                {
                                    type = "[time] NULL";
                                }
                                else if (itemField.fieldValueType == "Boolean" && (itemField.fieldType == "SwitchPicker"))
                                {
                                    type = "[bit] NULL";
                                }
                                else if (itemField.fieldValueType == "ThreeState" && (itemField.fieldType == "ThreeState"))
                                {
                                    type = "[bit] NULL";
                                }
                                else if (itemField.fieldValueType == "Uncomparable" && (itemField.fieldType == "LocationPicker"))
                                {
                                    type = "[nvarchar](150) NULL";
                                }
                                else if (itemField.fieldValueType == "Integer" && (itemField.fieldType == "RatingSelector"))
                                {
                                    type = "[float] NULL";
                                }

                                //query += $"\t[{(itemField.fieldKey).ToUpper()}] {type}, {System.Environment.NewLine}";
                                ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = (itemField.fieldKey).ToUpper(), Type = type });
                                counting_inputs += 1;

                                if (itemField.captureTimeStamp == true)
                                {
                                    //query += $"\t[{(itemField.fieldKey).ToUpper()}_TIMESTAMP] [datetime] NULL, {System.Environment.NewLine}";
                                    ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = ((itemField.fieldKey).ToUpper() + "_TIMESTAMP"), Type = "[datetime] NULL" });
                                    counting_inputs += 1;
                                }

                            }
                            else
                            {
                                if (itemField.fieldValueType == "Array" && (itemField.fieldType == "ListPicker" && itemField.allowMultiSelect == false))
                                {
                                    type = "[nvarchar](4000) NULL";
                                    ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = (itemField.fieldKey).ToUpper(), Type = type });
                                    //query += $"\t[{(itemField.fieldKey).ToUpper()}] {type}, {System.Environment.NewLine}";
                                    counting_inputs += 1;

                                    if (itemField.captureTimeStamp == true)
                                    {
                                        ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = ((itemField.fieldKey).ToUpper() + "_TIMESTAMP"), Type = "[datetime] NULL" });
                                        //query += $"\t[{(itemField.fieldKey).ToUpper()}_TIMESTAMP] [datetime] NULL, {System.Environment.NewLine}";
                                        counting_inputs += 1;
                                    }
                                }
                                else
                                {
                                    if (itemField.fieldType == "MultiPhotoPicker")
                                    {
                                        if (itemField.captureGeoLocation == true)
                                        {
                                            ListChilds.Add(new ListPossibleChilds { FieldKey = itemField.fieldKey, ParentTableName = MainTableName, GeoLocation = true, Type = "MultiPhotoPicker" });
                                        }
                                        else
                                        {
                                            ListChilds.Add(new ListPossibleChilds { FieldKey = itemField.fieldKey, ParentTableName = MainTableName, GeoLocation = false, Type = "MultiPhotoPicker" });
                                        }

                                    }

                                    if (itemField.fieldType == "ListPicker" && itemField.allowMultiSelect == true)
                                    {
                                        ListChilds.Add(new ListPossibleChilds { FieldKey = itemField.fieldKey, ParentTableName = MainTableName, GeoLocation = false, Type = "ListPicker" });
                                    }
                                }
                            }

                        }

                    }

                }
                else
                {
                    string jsonFormDefinition = MakeRequestWithBasicAuth(FastFieldURL + item1.Id, FastFieldUser, FastFieldPassword, ListHeaders);
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
                    ListForName = MainTableName.Split(" ").ToList();
                    ListForName = ListForName.Where(x => x != "").ToList();
                    ListForName = ListForName.ConvertAll(d => d.First().ToString().ToUpper() + d.Substring(1).ToLower());
                    MainTableName = ConvertDiacriticsAccent(String.Join("", ListForName));

                    foreach (var itemPage in FormDefinitionObject.formJson.page)
                    {
                        foreach (var itemSection in itemPage.section)
                        {
                            if (itemSection.field != null)
                            {
                                foreach (var itemField in itemSection.field)
                                {
                                    type = "[nvarchar](4000) NULL";

                                    if (itemField.fieldType == "TextLabel" && itemField.value != null)
                                    {
                                        type = "[nvarchar](4000) NULL";
                                        ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = (itemField.fieldKey).ToUpper(), Type = type });
                                        counting_inputs += 1;
                                    }

                                    if (itemField.fieldType != "MultiPhotoPicker" && (itemField.fieldType != "ListPicker") && itemField.fieldType != "TextLabel")
                                    {
                                        if (itemField.fieldValueType == "String" && itemField.fieldType == "SingleLineEntryAlpha")
                                        {
                                            if (itemField.enableMinMaxValues)
                                            {
                                                type = $"[nvarchar]({itemField.maxLength}) NULL";
                                            }
                                            else
                                            {
                                                type = "[nvarchar](4000) NULL";
                                            }
                                        }
                                        else if (itemField.fieldValueType == "String" && (itemField.fieldType == "MultiLineEntry" || itemField.fieldType == "VideoPicker" || itemField.fieldType == "AudioPicker"))
                                        {
                                            type = "[nvarchar](4000) NULL";
                                        }
                                        else if (itemField.fieldValueType == "Integer" && (itemField.fieldType == "SingleLineEntryNumeric" || itemField.fieldType == "FormattedInput" || itemField.fieldType == "ComputedLabel"))
                                        {
                                            type = "[nvarchar](4000) NULL";
                                        }
                                        else if (itemField.fieldValueType == "Double" && (itemField.fieldType == "SingleLineEntryNumeric" || itemField.fieldType == "ComputedLabel"))
                                        {
                                            type = "[nvarchar](4000) NULL";
                                        }
                                        else if (itemField.fieldValueType == "Uncomparable" && (itemField.fieldType == "PhotoPicker" || itemField.fieldType == "SignatureCapture" || itemField.fieldType == "InlinePhoto"))
                                        {
                                            type = "[nvarchar](4000) NULL";
                                        }
                                        else if (itemField.fieldValueType == "DateOnly" && (itemField.fieldType == "DatePicker"))
                                        {
                                            type = "[datetime] NULL";
                                        }
                                        else if (itemField.fieldValueType == "TimeOnly" && (itemField.fieldType == "DatePicker"))
                                        {
                                            type = "[time] NULL";
                                        }
                                        else if (itemField.fieldValueType == "Boolean" && (itemField.fieldType == "SwitchPicker"))
                                        {
                                            type = "[bit] NULL";
                                        }
                                        else if (itemField.fieldValueType == "Boolean" && (itemField.fieldType == "ThreeState"))
                                        {
                                            type = "[bit] NULL";
                                        }
                                        else if (itemField.fieldValueType == "Uncomparable" && (itemField.fieldType == "LocationPicker"))
                                        {
                                            type = "[nvarchar](150) NULL";
                                        }
                                        else if (itemField.fieldValueType == "Integer" && (itemField.fieldType == "RatingSelector"))
                                        {
                                            type = "[nvarchar](4000) NULL";
                                        }
                                        //query += $"\t[{(itemField.fieldKey).ToUpper()}] {type}, {System.Environment.NewLine}";
                                        ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = (itemField.fieldKey).ToUpper(), Type = type });
                                        counting_inputs += 1;

                                        if (itemField.captureTimeStamp == true)
                                        {
                                            //query += $"\t[{(itemField.fieldKey).ToUpper()}_TIMESTAMP] [datetime] NULL, {System.Environment.NewLine}";
                                            ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = ((itemField.fieldKey).ToUpper() + "_TIMESTAMP"), Type = "[datetime] NULL" });
                                            counting_inputs += 1;
                                        }

                                    }
                                    else
                                    {
                                        if (itemField.fieldValueType == "Array" && (itemField.fieldType == "ListPicker" && itemField.allowMultiSelect == false))
                                        {
                                            type = "[nvarchar](4000) NULL";
                                            ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = (itemField.fieldKey).ToUpper(), Type = type });
                                            //query += $"\t[{(itemField.fieldKey).ToUpper()}] {type}, {System.Environment.NewLine}";
                                            counting_inputs += 1;

                                            if (itemField.captureTimeStamp == true)
                                            {
                                                ListMainColumns.Add(new ListColumns { TableName = MainTableName, FieldKey = ((itemField.fieldKey).ToUpper() + "_TIMESTAMP"), Type = "[datetime] NULL" });
                                                //query += $"\t[{(itemField.fieldKey).ToUpper()}_TIMESTAMP] [datetime] NULL, {System.Environment.NewLine}";
                                                counting_inputs += 1;
                                            }
                                        }
                                        else
                                        {
                                            if (itemField.fieldType == "MultiPhotoPicker")
                                            {
                                                if (itemField.captureGeoLocation == true)
                                                {
                                                    ListChilds.Add(new ListPossibleChilds { FieldKey = itemField.fieldKey, ParentTableName = MainTableName, GeoLocation = true, Type = "MultiPhotoPicker" });
                                                }
                                                else
                                                {
                                                    ListChilds.Add(new ListPossibleChilds { FieldKey = itemField.fieldKey, ParentTableName = MainTableName, GeoLocation = false, Type = "MultiPhotoPicker" });
                                                }

                                            }

                                            if (itemField.fieldType == "ListPicker" && itemField.allowMultiSelect == true)
                                            {
                                                ListChilds.Add(new ListPossibleChilds { FieldKey = itemField.fieldKey, ParentTableName = MainTableName, GeoLocation = false, Type = "ListPicker" });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                string ChildsTableQuery = string.Empty;

                if (ListMainColumns.Count >= 450)
                {
                    int thirdpart = ListMainColumns.Count / 3;
                    int lastpart = ListMainColumns.Count - (thirdpart + thirdpart);
                    List<ListColumns> newlist1 = ListMainColumns.GetRange(0, thirdpart);
                    List<ListColumns> newlist2 = ListMainColumns.GetRange(thirdpart, thirdpart);
                    List<ListColumns> newlist3 = ListMainColumns.GetRange(thirdpart + thirdpart, lastpart);

                    query = string.Empty;

                    // Table 1
                    query += $"CREATE TABLE [{schema}].[{MainTableName}_1]( {System.Environment.NewLine}";
                    query += $"\t[ID] [int] IDENTITY(1,1) NOT NULL, {System.Environment.NewLine}";
                    query += $"\t[SUBMISSIONID] [nvarchar](150) NULL, {System.Environment.NewLine}";
                    query += $"\t[USERNAME] [nvarchar](512) NULL, {System.Environment.NewLine}";
                    query += $"\t[UPDATEDAT] [datetime] NULL, {System.Environment.NewLine}";
                    query += $"\t[FORMID] [int] NULL, {System.Environment.NewLine}";
                    query += $"\t[FORMNAME] [nvarchar](512) NULL, {System.Environment.NewLine}";
                    query += $"\t[STARTFORMTIMESTAMP] [datetime] NULL, {System.Environment.NewLine}";
                    query += $"\t[ENDFORMTIMESTAMP] [datetime] NULL, {System.Environment.NewLine}";

                    foreach (var itemlist1 in newlist1)
                    {
                        query += $"\t[{itemlist1.FieldKey}] {itemlist1.Type}, {System.Environment.NewLine}";
                    }

                    query += $"CONSTRAINT [PK_{MainTableName}_1] PRIMARY KEY CLUSTERED ( [Id] ASC ) ); ";
                    query += System.Environment.NewLine + System.Environment.NewLine;

                    // Table 2
                    query += $"CREATE TABLE [{schema}].[{MainTableName}_2]( {System.Environment.NewLine}";
                    query += $"\t[ID] [int] IDENTITY(1,1) NOT NULL, {System.Environment.NewLine}";
                    query += $"\t[SUBMISSIONID] [nvarchar](150) NULL, {System.Environment.NewLine}";

                    foreach (var itemlist2 in newlist2)
                    {
                        query += $"\t[{itemlist2.FieldKey}] {itemlist2.Type}, {System.Environment.NewLine}";
                    }

                    query += $"CONSTRAINT [PK_{MainTableName}_2] PRIMARY KEY CLUSTERED ( [Id] ASC ) ); ";
                    query += System.Environment.NewLine + System.Environment.NewLine;

                    // Table 3
                    query += $"CREATE TABLE [{schema}].[{MainTableName}_3]( {System.Environment.NewLine}";
                    query += $"\t[ID] [int] IDENTITY(1,1) NOT NULL, {System.Environment.NewLine}";
                    query += $"\t[SUBMISSIONID] [nvarchar](150) NULL, {System.Environment.NewLine}";
                    log.LogInformation($"Generating query for table: [{schema}].[{MainTableName}]");

                    foreach (var itemlist3 in newlist3)
                    {
                        query += $"\t[{itemlist3.FieldKey}] {itemlist3.Type}, {System.Environment.NewLine}";
                    }

                    query += $"CONSTRAINT [PK_{MainTableName}_3] PRIMARY KEY CLUSTERED ( [Id] ASC ) ); ";
                    query += System.Environment.NewLine + System.Environment.NewLine;

                }
                else
                {
                    query = string.Empty;

                    // Table 1
                    query += $"CREATE TABLE [{schema}].[{MainTableName}]( {System.Environment.NewLine}";
                    query += $"\t[ID] [int] IDENTITY(1,1) NOT NULL, {System.Environment.NewLine}";
                    query += $"\t[SUBMISSIONID] [nvarchar](150) NULL, {System.Environment.NewLine}";
                    query += $"\t[USERNAME] [nvarchar](512) NULL, {System.Environment.NewLine}";
                    query += $"\t[UPDATEDAT] [datetime] NULL, {System.Environment.NewLine}";
                    query += $"\t[FORMID] [int] NULL, {System.Environment.NewLine}";
                    query += $"\t[FORMNAME] [nvarchar](512) NULL, {System.Environment.NewLine}";
                    query += $"\t[STARTFORMTIMESTAMP] [datetime] NULL, {System.Environment.NewLine}";
                    query += $"\t[ENDFORMTIMESTAMP] [datetime] NULL, {System.Environment.NewLine}";
                    log.LogInformation($"Generating query for table: [{schema}].[{MainTableName}]");

                    foreach (var itemlist in ListMainColumns)
                    {
                        query += $"\t[{itemlist.FieldKey}] {itemlist.Type}, {System.Environment.NewLine}";
                    }

                    query += $"CONSTRAINT [PK_{MainTableName}] PRIMARY KEY CLUSTERED ( [Id] ASC ) ); ";
                    query += System.Environment.NewLine + System.Environment.NewLine;
                }

                foreach (var itemChild in ListChilds)
                {
                    if (itemChild.Type == "MultiPhotoPicker")
                    {
                        if (itemChild.GeoLocation == true)
                        {
                            ChildsTableQuery += $"CREATE TABLE [{schema}].[{itemChild.ParentTableName + "_" + itemChild.FieldKey}] ( \n" +
                                $"\t[Id] [int] IDENTITY(1, 1) NOT NULL, \n" +
                                $"\t[submissionId] nvarchar(36) NULL, \n" +
                                $"\t[{itemChild.FieldKey}_photo] nvarchar(512) NULL, \n" +
                                $"\t[{itemChild.FieldKey}_comment] nvarchar(4000) NULL, \n" +
                                $"\t[{itemChild.FieldKey}_latitude] float NULL, \n" +
                                $"\t[{itemChild.FieldKey}_longitude] float NULL, \n" +
                                $"CONSTRAINT [PK_{itemChild.ParentTableName + "_" + itemChild.FieldKey}] PRIMARY KEY CLUSTERED ( [Id] ASC ) ); ";
                            ChildsTableQuery += System.Environment.NewLine + System.Environment.NewLine;
                        }
                        else
                        {
                            ChildsTableQuery += $"CREATE TABLE [{schema}].[{itemChild.ParentTableName + "_" + itemChild.FieldKey}] ( \n" +
                                $"\t[Id] [int] IDENTITY(1, 1) NOT NULL, \n" +
                                $"\t[submissionId] nvarchar(36) NULL, \n" +
                                $"\t[{itemChild.FieldKey}_photo] nvarchar(512) NULL, \n" +
                                $"\t[{itemChild.FieldKey}_comment] nvarchar(4000) NULL, \n" +
                                $"CONSTRAINT [PK_{itemChild.ParentTableName + "_" + itemChild.FieldKey}] PRIMARY KEY CLUSTERED ( [Id] ASC ) ); ";
                            ChildsTableQuery += System.Environment.NewLine + System.Environment.NewLine;
                        }
                    }
                    else
                    {
                        ChildsTableQuery += $"CREATE TABLE [{schema}].[{itemChild.ParentTableName + "_" + itemChild.FieldKey}] ( \n" +
                                $"\t[Id] [int] IDENTITY(1, 1) NOT NULL, \n" +
                                $"\t[submissionId] nvarchar(36) NULL, \n" +
                                $"\t[{itemChild.FieldKey}] nvarchar(4000) NULL, \n" +
                                $"CONSTRAINT [PK_{itemChild.ParentTableName + "_" + itemChild.FieldKey}] PRIMARY KEY CLUSTERED ( [Id] ASC ) ); ";
                        ChildsTableQuery += System.Environment.NewLine + System.Environment.NewLine;
                    }

                }

                string tableSubmissionsId = $"""
                CREATE TABLE [{schema}].[{MainTableName}_SubmissionId](
                    	[Id] [int] IDENTITY(1,1) NOT NULL,
                    	[SubmissionId] [nvarchar](50) NULL,
                    	[FormName] [nvarchar](2000) NULL,
                    	[SubmittedOn] [datetime] NULL,
                CONSTRAINT [PK_{MainTableName}_SubmissionId] PRIMARY KEY CLUSTERED ( [Id] ASC )) ON [PRIMARY];
                """;

                log.LogInformation($"Total Columns: {counting_inputs} \n");
                string AllQuery = query + ChildsTableQuery + tableSubmissionsId;

                SqlDataReader reader1;

                // EXECUTE QUERY DATA INSERT OF MAIN TABLE 
                try
                {
                    if (cnn.State == ConnectionState.Open)
                    {
                        Thread.Sleep(500);
                        cnn.Close();
                        Thread.Sleep(500);
                    }
                    cnn.Open();
                    command = new SqlCommand(AllQuery, cnn)
                    {
                        CommandTimeout = 3600
                    };
                    reader1 = command.ExecuteReader();
                    Thread.Sleep(1000);

                    log.LogInformation("The API Forms from FastField schema was send to DataBase.");

                    cnn.Close();
                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    log.LogError($"{ex.Message}");
                    return new ObjectResult(new
                    {
                        error = ex.Message
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

            }

            log.LogInformation($"Todas las tablas generadas fueron ejecutadas exitósamente.");

            log.LogInformation($"Function executed successfully. ");
            Thread.Sleep(500);
            return new OkObjectResult($"Function executed successfully. ");

        }


        private static string MakeRequestAuthenticate(string url, string applicationId, string password, List<Headers> headers)
        {
            var client = new HttpClient();

            // Request headers
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
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
                    string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
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

        public class ListColumns
        {
            public string TableName { get; set; }
            public string FieldKey { get; set; }
            public string Type { get; set; }
        }


    }
}
