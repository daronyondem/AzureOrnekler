using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ImageObjectFiltering
{
    public static class SingleImageAnalyzer
    {
        [FunctionName("SingleImageAnalyzer")]
        public static async Task<string> Run([ActivityTrigger] DurableActivityContext context, TraceWriter log)
        {
            var imageUrl = context.GetInput<string>();

            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(System.Environment.GetEnvironmentVariable("VisionAPIKey")))
            {
                Endpoint = "https://westeurope.api.cognitive.microsoft.com"
            };
            var analysis = await client.AnalyzeImageAsync(imageUrl).ConfigureAwait(false);

            if(analysis.Categories.Any(x=> x.Name.Contains("food_")))
            {
                return imageUrl;
            }
            else
            {
                return null;
            }
        }
    }
}