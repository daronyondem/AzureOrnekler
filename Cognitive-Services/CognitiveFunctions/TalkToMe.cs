using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CognitiveFunctions
{
    public static class TalkToMe
    {
        [FunctionName("TalkToMe")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext context)
        {
            #region Preparation
            var textToRead = req.Query["text"];

            var config = new ConfigurationBuilder()
              .SetBasePath(context.FunctionAppDirectory)
              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            var ttsAuthEndPoint = config["ttsAuthEndPoint"];
            var ttsSubscriptionKey = config["ttsSubscriptionKey"];
            var ttsEndPoint = config["ttsEndPoint"];

            #endregion

            string authToken;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ttsSubscriptionKey);
                UriBuilder uriBuilder = new UriBuilder(ttsAuthEndPoint);

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                authToken = await result.Content.ReadAsStringAsync();
                
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");
                client.DefaultRequestHeaders.Add("User-Agent", "Daron TalkToMe Test Function");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");

                var ssmlXmlTemplate = @"<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
                                            <voice name='Microsoft Server Speech Text to Speech Voice (tr-TR, SedaRUS)'>
                                                {0}
                                            </voice>
                                        </speak> ";
                var ssmlXml = string.Format(ssmlXmlTemplate, textToRead);
                result = await client.PostAsync(ttsEndPoint, new StringContent(ssmlXml));
                return new FileContentResult(await result.Content.ReadAsByteArrayAsync(), "audio/mpg");
            }
        }
    }
}
