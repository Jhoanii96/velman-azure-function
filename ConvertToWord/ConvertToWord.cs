using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DocSharp.Markdown;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace VelmanAzureFunction.ConvertToWord
{
    public static class ConvertToWord
    {

        [Function("ConvertToWord")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, FunctionContext context)
        {
            var log = context.GetLogger("ConvertToWord");
            try
            {
                // ------------------------------------------------------------
                // 1. Read Markdown (ASYNC – required by Azure Functions)
                // ------------------------------------------------------------
                string markdown = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrWhiteSpace(markdown))
                    return new BadRequestObjectResult("Markdown text is required.");

                // ------------------------------------------------------------
                // 2. Markdown → Word (.docx) using DocSharp.Markdown
                // ------------------------------------------------------------
                byte[] wordBytes;

                var converter = new MarkdownConverter();
                var markdown_source = MarkdownSource.FromMarkdownString(markdown);

                using (var wordStream = new MemoryStream())
                {
                    converter.ToDocx(markdown_source, wordStream);
                    wordBytes = wordStream.ToArray();
                }

                // ------------------------------------------------------------
                // 3. Upload to Azure Blob Storage
                // ------------------------------------------------------------
                string connectionString =
                    Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? "";

                string containerName = "reports";
                string fileName = $"report-{Guid.NewGuid()}.docx";

                BlobContainerClient container =
                    new BlobContainerClient(connectionString, containerName);

                await container.CreateIfNotExistsAsync();

                BlobClient blob = container.GetBlobClient(fileName);

                using (var uploadStream = new MemoryStream(wordBytes))
                {
                    await blob.UploadAsync(uploadStream, overwrite: true);
                }

                // ------------------------------------------------------------
                // 4. Generate SAS URL (read-only, 24h)
                // ------------------------------------------------------------
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = fileName,
                    Resource = "b",
                    ExpiresOn = DateTime.UtcNow.AddHours(24)
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                Uri sasUri = blob.GenerateSasUri(sasBuilder);

                // ------------------------------------------------------------
                // 5. Log generated URL
                // ------------------------------------------------------------
                log.LogInformation($"New report generated URL: {sasUri}");

                // ------------------------------------------------------------
                // 6. Return URL
                // ------------------------------------------------------------
                return new OkObjectResult(new
                {
                    fileUrl = sasUri.ToString(),
                    fileName,
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                });
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error generating Markdown report");

                return new ObjectResult(new
                {
                    error = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

        }



    }
}
