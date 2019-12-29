
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CognitiveFunctions
{
    public static class IsItACar
    {
        [FunctionName("IsItACar")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext context)
        {
            #region Preparation
            var imageUrl = req.Query["imageUrl"];

            var config = new ConfigurationBuilder()
              .SetBasePath(context.FunctionAppDirectory)
              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            var predictionSubscriptionKey = config["predictionSubscriptionKey"];
            var predictionProjectId = config["predictionProjectId"];

            #endregion

            PredictionEndpoint endpoint = new PredictionEndpoint() { ApiKey = predictionSubscriptionKey };

            var result = endpoint.PredictImageUrl(Guid.Parse(predictionProjectId), new ImageUrl(imageUrl));

            #region Output
            return new OkObjectResult(result);
            #endregion
        }
    }
}
