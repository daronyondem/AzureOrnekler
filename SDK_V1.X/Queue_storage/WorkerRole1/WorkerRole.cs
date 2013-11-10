using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            var Client = storageAccount.CreateCloudQueueClient();
            CloudQueue q = Client.GetQueueReference("newqueue");

            DateTime LastExecution = DateTime.Now;


            while (true)
            {
                //CloudQueueMessage m = q.PeekMessage();
                //BirListe = q.PeekMessages(10);

                CloudQueueMessage currentMessage = q.GetMessage();

                if (currentMessage != null)
                {
                    string s = currentMessage.AsString;

                    Trace.WriteLine(string.Format("Incommind Message : {0}", s), "Information");
                    Trace.Write(LastExecution.Subtract(DateTime.Now).TotalSeconds.ToString());
                    LastExecution = DateTime.Now;
                    //currentMessage.SetMessageContent("asdasdasd");
                    //q.UpdateMessage(currentMessage, TimeSpan.FromSeconds(40), MessageUpdateFields.Visibility);

                    q.DeleteMessage(currentMessage);
                }
                else
                {
                    Thread.Sleep(10000);
                }
                
                //q.RetrieveApproximateMessageCount();
                
               
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSettingPublisher) =>
            {
                var connectionString = RoleEnvironment.GetConfigurationSettingValue(configName);
                configSettingPublisher(connectionString);
            }
            );
            return base.OnStart();
        }
    }
}
