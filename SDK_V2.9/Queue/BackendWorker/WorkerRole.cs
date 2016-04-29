using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace BackendWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("BackendWorker is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            bool result = base.OnStart();

            Trace.TraceInformation("BackendWorker has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("BackendWorker is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("BackendWorker has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {             
                CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
                CloudQueueClient queueClient = account.CreateCloudQueueClient();
                CloudQueue q = queueClient.GetQueueReference("jobqueue");
                await q.CreateIfNotExistsAsync();
                CloudQueueMessage currentMessage = await q.GetMessageAsync();
                if (currentMessage != null)
                {
                    Trace.TraceInformation(currentMessage.AsString);
                    await q.DeleteMessageAsync(currentMessage);
                }               
                await Task.Delay(1000);
            }
        }
    }
}
