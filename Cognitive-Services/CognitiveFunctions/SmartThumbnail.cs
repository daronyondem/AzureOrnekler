using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace CognitiveFunctions
{
    public static class SmartThumbnail
    {
        [FunctionName("SmartThumbnail")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext context)
        {
            #region Preparation
            var imageUrl = req.Query["imageUrl"];

            var config = new ConfigurationBuilder()
              .SetBasePath(context.FunctionAppDirectory)
              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            var visionEndpoint = config["visionEndpoint"];
            var visionSubscriptionKey = config["visionSubscriptionKey"];

            #endregion

            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(visionSubscriptionKey), new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = visionEndpoint
            };

            Stream thumbnail = await computerVision.GenerateThumbnailAsync(
                500, 500, imageUrl, true);

            #region Output
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = thumbnail.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return new FileContentResult(ms.ToArray(), "image/jpeg");
            }
#endregion
        }
    }
}
