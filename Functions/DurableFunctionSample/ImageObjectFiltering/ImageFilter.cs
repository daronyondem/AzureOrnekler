using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImageObjectFiltering
{
    public static class ImageFilter
    {
        [FunctionName("ImageFilter")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, [OrchestrationClient]DurableOrchestrationClient starter,TraceWriter log)
        {
            dynamic body = await req.Content.ReadAsStringAsync().ConfigureAwait(false);
            var imageUrlList = JsonConvert.DeserializeObject<List<string>>(body as string);

            string instanceId = await starter.StartNewAsync("ImageAnalysisOrchestrator", imageUrlList).ConfigureAwait(false);

            return req.CreateResponse(HttpStatusCode.RequestTimeout, instanceId);
        }
    }
}
