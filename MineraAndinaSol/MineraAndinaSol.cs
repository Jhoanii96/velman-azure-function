using KomatsuAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static KomatsuAPI.Schema;

namespace VisionLinkReader.MineraAndinaSol
{
    public static class MineraAndinaSol
    {
        private static string sqlServerName, sqlDatabase, sqlLogin, sqlPassword;
        private static string urlRoot, urlBaseTimeSeries, komatsuId, schema, startDate, endDate, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix;


        [Function("MineraAndinaSol")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {

                sqlServerName = req.Headers["SqlServername"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("AzuSQLServerName");
                sqlDatabase = req.Headers["SqlDatabase"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("AzuSQLDatabase");
                sqlLogin = req.Headers["SqlLogin"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("AzuSQLUser");
                sqlPassword = req.Headers["SqlPassword"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("AzuSQLSecret");

                urlRoot = req.Headers["UrlRoot"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("KomatsuUrlRoot");
                urlBaseTimeSeries = (req.Headers["UrlBaseTimeSeries"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("KomatsuUrlBaseTimeSeries") ?? "").Trim();
                int indexurl = urlBaseTimeSeries.LastIndexOf('/');
                if ((urlBaseTimeSeries.Length - 1) == urlBaseTimeSeries.LastIndexOf('/'))
                {
                    urlBaseTimeSeries = urlBaseTimeSeries.Remove(indexurl, 1);
                }
                komatsuId = req.Headers["KomatsuId"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("KomatsuId");
                schema = req.Headers["SchemaTable"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("KomatsuSchema") ?? "kom";
                startDate = req.Headers["StartDate"].FirstOrDefault();
                endDate = req.Headers["EndDate"].FirstOrDefault();
                if (string.IsNullOrEmpty(endDate))
                {
                    log.LogInformation($"End Date: {DateTime.Now:yyyy-MM-dd}");
                }
                else
                {
                    log.LogInformation($"End Date: {endDate}");
                }

                grandType = "Password Credentials";
                accessTokenUrl = req.Headers["TokenUrl"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("KomatsuTokenUrl");
                clientId = string.Empty;
                clientSecret = string.Empty;
                username = req.Headers["Username"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("KomatsuUsername");
                password = req.Headers["Password"].FirstOrDefault()
                    ?? Environment.GetEnvironmentVariable("KomatsuPassword");
                headerPrefix = "Bearer";

                log.LogInformation("Servername: " + sqlServerName);
                log.LogInformation($"Database: {sqlDatabase}");
                log.LogInformation($"Actual API URL: {urlRoot}");
                log.LogInformation($"Schema for table: {schema}");
                log.LogInformation($"Start Date: {startDate}");


                // Create an instance of the SqlExecutor
                SqlExecutor executor = new SqlExecutor(sqlServerName, sqlDatabase, sqlLogin, sqlPassword);

                var EquipmentJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                EquipmentClass.Rootobject EquipmentObject = JsonConvert.DeserializeObject<EquipmentClass.Rootobject>(EquipmentJson);

                // -------------------------- CREATING QUERY INSERT MERGE TABLE STATEMENT -------------------------- //
                string query_data_middle = string.Empty;
                string query_all = string.Empty;

                foreach (var item in EquipmentObject.Equipment)
                {

                    query_data_middle += $"\t('{item.EquipmentHeader.OEMName}', '{item.EquipmentHeader.Model}', '{item.EquipmentHeader.EquipmentID}', '{item.EquipmentHeader.SerialNumber}'), {System.Environment.NewLine}";
                    int index1 = query_data_middle.LastIndexOf(',');
                    query_data_middle = query_data_middle.Remove(index1, 1);
                    query_data_middle = query_data_middle.Replace("''", "NULL");
                    query_all += Komatsu_Equipment(schema, query_data_middle);
                    log.LogInformation($"Equipment [{item.EquipmentHeader.Model}]");
                    Thread.Sleep(2000);
                    if (string.IsNullOrEmpty(endDate))
                    {
                        urlRoot = $"{urlBaseTimeSeries}/" +
                            $"{komatsuId}/{item.EquipmentHeader.Model}/{item.EquipmentHeader.SerialNumber}/{startDate}/{DateTime.Now:yyyy-MM-dd}/";
                    }
                    else
                    {
                        urlRoot = $"{urlBaseTimeSeries}/" +
                            $"{komatsuId}/{item.EquipmentHeader.Model}/{item.EquipmentHeader.SerialNumber}/{startDate}/{endDate}/";
                    }

                    var TimeSeriesJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                    TimeSeriesClass.Rootobject TimeSeriesObject = JsonConvert.DeserializeObject<TimeSeriesClass.Rootobject>(TimeSeriesJson);

                    if (TimeSeriesObject.Equipment != null)
                    {
                        log.LogInformation($"Append data details...");
                        foreach (var itemLinks in TimeSeriesObject.Equipment[0].Links)
                        {
                            if (itemLinks.rel == "Locations")
                            {
                                log.LogInformation($"Equipment {item.EquipmentHeader.Model} -> Locations.");
                                urlRoot = itemLinks.href;
                                string LocationsJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                                LocationsClass.Rootobject LocationsObject = JsonConvert.DeserializeObject<LocationsClass.Rootobject>(LocationsJson);

                                string query_loc = string.Empty;

                                foreach (var itemLoc in LocationsObject.Location)
                                {
                                    query_loc += $"\t('{item.EquipmentHeader.SerialNumber}', '{itemLoc.datetime:yyyy-MM-dd HH:mm:ss}', '{itemLoc.Latitude}', '{itemLoc.Longitude}'), {System.Environment.NewLine}";
                                }

                                int indexLoc = query_loc.LastIndexOf(',');
                                query_loc = query_loc.Remove(indexLoc, 1);
                                query_loc = query_loc.Replace("''", "NULL");

                                query_all += Environment.NewLine + Environment.NewLine;
                                query_all += Komatsu_Locations(schema, query_loc);

                            }
                            if (itemLinks.rel == "CumulativeOperatingHours")
                            {
                                log.LogInformation($"Equipment {item.EquipmentHeader.Model} -> CumulativeOperatingHours.");
                                urlRoot = itemLinks.href;
                                string CumulativeOperatingHoursJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                                CumulativeOperatingHoursClass.Rootobject CumulativeOperatingHoursClassObject = JsonConvert.DeserializeObject<CumulativeOperatingHoursClass.Rootobject>(CumulativeOperatingHoursJson);

                                string query_coh = string.Empty;

                                foreach (var itemCoh in CumulativeOperatingHoursClassObject.CumulativeOperatingHours)
                                {
                                    query_coh += $"\t('{item.EquipmentHeader.SerialNumber}', '{itemCoh.datetime:yyyy-MM-dd HH:mm:ss}', '{itemCoh.Hour}'), {System.Environment.NewLine}";
                                }

                                int indexCoh = query_coh.LastIndexOf(',');
                                query_coh = query_coh.Remove(indexCoh, 1);
                                query_coh = query_coh.Replace("''", "NULL");

                                query_all += Environment.NewLine + Environment.NewLine;
                                query_all += Komatsu_CumulativeOperatingHours(schema, query_coh);

                            }
                            if (itemLinks.rel == "CumulativeFuelUsed")
                            {
                                log.LogInformation($"Equipment {item.EquipmentHeader.Model} -> CumulativeFuelUsed.");
                                urlRoot = itemLinks.href;
                                string CumulativeFuelUsedJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                                CumulativeFuelUsedClass.Rootobject CumulativeFuelUsedObject = JsonConvert.DeserializeObject<CumulativeFuelUsedClass.Rootobject>(CumulativeFuelUsedJson);

                                string query_fu = string.Empty;

                                foreach (var itemFu in CumulativeFuelUsedObject.FuelUsed)
                                {
                                    query_fu += $"\t('{item.EquipmentHeader.SerialNumber}', '{itemFu.datetime:yyyy-MM-dd HH:mm:ss}', '{itemFu.FuelUnits}', '{itemFu.FuelConsumed}'), {System.Environment.NewLine}";
                                }

                                int indexFu = query_fu.LastIndexOf(',');
                                query_fu = query_fu.Remove(indexFu, 1);
                                query_fu = query_fu.Replace("''", "NULL");

                                query_all += Environment.NewLine + Environment.NewLine;
                                query_all += Komatsu_CumulativeFuelUsed(schema, query_fu);

                            }
                            if (itemLinks.rel == "FuelUsedInThePreceding24Hours")
                            {
                                log.LogInformation($"Equipment {item.EquipmentHeader.Model} -> FuelUsedInThePreceding24Hours.");
                                urlRoot = itemLinks.href;
                                string FuelUsedInThePreceding24HoursJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                                FuelUsedInThePreceding24HoursClass.Rootobject FuelUsedInThePreceding24HoursObject = JsonConvert.DeserializeObject<FuelUsedInThePreceding24HoursClass.Rootobject>(FuelUsedInThePreceding24HoursJson);

                                string query_ful = string.Empty;

                                foreach (var itemFul in FuelUsedInThePreceding24HoursObject.FuelUsedLast24)
                                {
                                    query_ful += $"\t('{item.EquipmentHeader.SerialNumber}', '{itemFul.datetime:yyyy-MM-dd HH:mm:ss}', '{itemFul.FuelUnits}', '{itemFul.FuelConsumed}'), {System.Environment.NewLine}";
                                }

                                int indexFul = query_ful.LastIndexOf(',');
                                query_ful = query_ful.Remove(indexFul, 1);
                                query_ful = query_ful.Replace("''", "NULL");

                                query_all += Environment.NewLine + Environment.NewLine;
                                query_all += Komatsu_FuelUsedInThePreceding24Hours(schema, query_ful);

                            }
                            if (itemLinks.rel == "FuelRemainingRatio")
                            {
                                log.LogInformation($"Equipment {item.EquipmentHeader.Model} -> FuelRemainingRatio.");
                                urlRoot = itemLinks.href;
                                string FuelRemainingRatioJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                                FuelRemainingRatioClass.Rootobject FuelRemainingRatioObject = JsonConvert.DeserializeObject<FuelRemainingRatioClass.Rootobject>(FuelRemainingRatioJson);

                                string query_fro = string.Empty;

                                foreach (var itemFro in FuelRemainingRatioObject.FuelRemaining)
                                {
                                    query_fro += $"\t('{item.EquipmentHeader.SerialNumber}', '{itemFro.datetime:yyyy-MM-dd HH:mm:ss}', '{itemFro.Percent}'), {System.Environment.NewLine}";
                                }

                                int indexFro = query_fro.LastIndexOf(',');
                                query_fro = query_fro.Remove(indexFro, 1);
                                query_fro = query_fro.Replace("''", "NULL");

                                query_all += Environment.NewLine + Environment.NewLine;
                                query_all += Komatsu_FuelRemainingRatio(schema, query_fro);

                            }
                            if (itemLinks.rel == "CumulativeNonProductiveIdleHours")
                            {
                                log.LogInformation($"Equipment {item.EquipmentHeader.Model} -> CumulativeNonProductiveIdleHours.");
                                urlRoot = itemLinks.href;
                                string CumulativeNonProductiveIdleHoursJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                                CumulativeNonProductiveIdleHoursClass.Rootobject CumulativeNonProductiveIdleHoursObject = JsonConvert.DeserializeObject<CumulativeNonProductiveIdleHoursClass.Rootobject>(CumulativeNonProductiveIdleHoursJson);

                                string query_cnp = string.Empty;

                                foreach (var itemCnp in CumulativeNonProductiveIdleHoursObject.CumulativeNonproductiveIdleHours)
                                {
                                    query_cnp += $"\t('{item.EquipmentHeader.SerialNumber}', '{itemCnp.datetime:yyyy-MM-dd HH:mm:ss}', '{itemCnp.Hour}'), {System.Environment.NewLine}";
                                }

                                int indexCnp = query_cnp.LastIndexOf(',');
                                query_cnp = query_cnp.Remove(indexCnp, 1);
                                query_cnp = query_cnp.Replace("''", "NULL");

                                query_all += Environment.NewLine + Environment.NewLine;
                                query_all += Komatsu_CumulativeNonProductiveIdleHours(schema, query_cnp);

                            }
                            if (itemLinks.rel == "Faults")
                            {
                                log.LogInformation($"Equipment {item.EquipmentHeader.Model} -> Faults.");
                                urlRoot = itemLinks.href;
                                string FaultsJson = await OAuthClient.MakeRequestWithOAuth20Async(urlRoot, grandType, accessTokenUrl, clientId, clientSecret, username, password, headerPrefix);
                                FaultsClass.Rootobject FaultsObject = JsonConvert.DeserializeObject<FaultsClass.Rootobject>(FaultsJson);

                                string query_fc = string.Empty;

                                foreach (var itemFc in FaultsObject.FaultCode)
                                {
                                    query_fc += $"\t('{item.EquipmentHeader.SerialNumber}', '{itemFc.datetime:yyyy-MM-dd HH:mm:ss}', '{itemFc.CodeIdentifier}', '{itemFc.CodeDescription.Replace("'", "''")}', '{itemFc.CodeSeverity}', '{itemFc.CodeSource}'), {System.Environment.NewLine}";
                                }

                                int indexFc = query_fc.LastIndexOf(',');
                                query_fc = query_fc.Remove(indexFc, 1);
                                query_fc = query_fc.Replace("''", "NULL");

                                query_all += Environment.NewLine + Environment.NewLine;
                                query_all += Komatsu_Faults(schema, query_fc);

                            }
                        }
                        Thread.Sleep(2000);

                    }

                    // Execute Query
                    executor.ExecuteMergeCommand(query_all);

                    query_data_middle = string.Empty;
                    query_all = string.Empty;

                }

                log.LogInformation($"Closing...");
                Thread.Sleep(5000);

                return new OkObjectResult("This HTTP triggered function executed successfully");

            }
            catch (Exception ex)
            {
                log.LogError("Error: " + ex.ToString());
                Thread.Sleep(5000);
                return new OkObjectResult(ex.ToString());
            }
        }
    }
}
