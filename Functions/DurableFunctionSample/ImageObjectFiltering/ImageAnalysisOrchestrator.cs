using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ImageObjectFiltering
{
    public static class ImageAnalysisOrchestrator
    {
        private const int PageSize = 10;

        [FunctionName("ImageAnalysisOrchestrator")]
        public static async void Run([OrchestrationTrigger] DurableOrchestrationContext context, TraceWriter log)
        {
            var imageUrlList = context.GetInput<List<string>>();
            List<string> tobeProcessed = imageUrlList.Take(PageSize).ToList();
                        
            var tasks = new Task<string>[tobeProcessed.Count];
            for (int i = 0; i < tobeProcessed.Count; i++)
            {
                tasks[i] = context.CallActivityAsync<string>("SingleImageAnalyzer", tobeProcessed[i]);
            }

            var stopwatch = Stopwatch.StartNew();
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            DateTime nextRound = context.CurrentUtcDateTime.AddMilliseconds(1000-stopwatch.Elapsed.TotalMilliseconds);
            await context.CreateTimer(nextRound, CancellationToken.None);

            var leftOverList = imageUrlList.Skip(PageSize).ToList();
            if(leftOverList.Count>0)
            {
                context.ContinueAsNew(leftOverList);
            }
        }
    }
}

