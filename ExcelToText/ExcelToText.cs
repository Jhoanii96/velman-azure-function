using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;

public class ExcelToText
{
    private readonly ILogger _logger;

    public ExcelToText(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ExcelToText>();
    }

    [Function("ExcelToText")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        _logger.LogInformation("Processing Excel file");

        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var json = JsonDocument.Parse(requestBody);

            if (!json.RootElement.TryGetProperty("fileContent", out var fileContent))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            byte[] excelBytes = Convert.FromBase64String(fileContent.GetString());

            var sb = new StringBuilder();

            using var stream = new MemoryStream(excelBytes);
            using var workbook = new XLWorkbook(stream);

            foreach (var sheet in workbook.Worksheets)
            {
                sb.AppendLine($"=== SHEET: {sheet.Name} ===");

                var range = sheet.RangeUsed();
                if (range == null)
                {
                    sb.AppendLine("(Empty sheet)");
                    continue;
                }

                foreach (var row in range.Rows())
                {
                    var rowValues = row.Cells()
                        .Select(c => c.GetValue<string>())
                        .ToArray();

                    sb.AppendLine(string.Join("\t", rowValues));
                }

                sb.AppendLine();
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            await response.WriteStringAsync(sb.ToString());

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing Excel");

            var error = req.CreateResponse(HttpStatusCode.InternalServerError);
            await error.WriteStringAsync($"Error: {ex.Message}");
            return error;
        }
    }
}
