using ClosedXML.Excel;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Net;
using System.Text.Json;

public class ExcelToPdf
{
    private readonly ILogger _logger;

    public ExcelToPdf(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ExcelToPdf>();
        QuestPDF.Settings.License = LicenseType.Community;
    }

    [Function("ExcelToPdf")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        try
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var json = JsonDocument.Parse(body);

            if (!json.RootElement.TryGetProperty("fileContent", out var fileContent))
                return req.CreateResponse(HttpStatusCode.BadRequest);

            byte[] excelBytes = Convert.FromBase64String(fileContent.GetString());

            using var excelStream = new MemoryStream(excelBytes);
            using var workbook = new XLWorkbook(excelStream);

            using var pdfStream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Content().Column(col =>
                    {
                        foreach (var sheet in workbook.Worksheets)
                        {
                            col.Item().Text($"SHEET: {sheet.Name}")
                                .Bold().FontSize(14);

                            var range = sheet.RangeUsed();
                            if (range == null)
                            {
                                col.Item().Text("(Empty sheet)");
                                continue;
                            }

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    for (int i = 0; i < range.ColumnCount(); i++)
                                        columns.RelativeColumn();
                                });

                                foreach (var row in range.Rows())
                                {
                                    foreach (var cell in row.Cells())
                                    {
                                        table.Cell().Border(1).Padding(3)
                                            .Text(cell.GetValue<string>() ?? "");
                                    }
                                }
                            });

                            col.Item().PageBreak();
                        }
                    });
                });
            }).GeneratePdf(pdfStream);

            var pdfBytes = pdfStream.ToArray();

            var fileObject = new
            {
                Content = Convert.ToBase64String(pdfBytes),   // base64
                ContentType = "application/pdf",
                Name = "excel-output.pdf",
                Value = Convert.ToBase64String(pdfBytes)      // required duplicate for Copilot
            };

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");

            await response.WriteStringAsync(
                JsonSerializer.Serialize(fileObject)
            );
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PDF generation failed");
            var error = req.CreateResponse(HttpStatusCode.InternalServerError);
            await error.WriteStringAsync(ex.Message);
            return error;
        }
    }
}
