using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CognitiveFunctions
{
    public static class HowUFeelin
    {
        [FunctionName("HowUFeelin")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log, Microsoft.Azure.WebJobs.ExecutionContext context)
        {
            #region Preparation
            var imageUrl = req.Query["imageUrl"];

            var config = new ConfigurationBuilder()
              .SetBasePath(context.FunctionAppDirectory)
              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            var textAnalyticsSubscriptionKey = config["textAnalyticsSubscriptionKey"];
            var textAnalyticsEndPoint = config["textAnalyticsEndPoint"];

            #endregion

            ITextAnalyticsClient client = new TextAnalyticsClient(new ApiKeyServiceClientCredentials(textAnalyticsSubscriptionKey))
            {
                Endpoint = textAnalyticsEndPoint
            };
            var result = await client.SentimentAsync(new MultiLanguageBatchInput(
                                        new List<MultiLanguageInput>()
                                        {
                                          new MultiLanguageInput("tr", "0", "Hava çok güzel!"),
                                          new MultiLanguageInput("tr", "1", "Hava rezalet!"),
                                          new MultiLanguageInput("tr", "2", "Hava nasıl?"),
                                        }));

            return new OkObjectResult(result);
        }

        class ApiKeyServiceClientCredentials : ServiceClientCredentials
        {
            private readonly string subscriptionKey;
            public ApiKeyServiceClientCredentials(string subscriptionKey)
            {
                this.subscriptionKey = subscriptionKey;
            }

            public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                return base.ProcessHttpRequestAsync(request, cancellationToken);
            }
        }
    }
}
