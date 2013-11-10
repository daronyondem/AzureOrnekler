using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace WebRole1
{
    public partial class start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            var Client = storageAccount.CreateCloudQueueClient();
            CloudQueue q = Client.GetQueueReference("newqueue");
            q.CreateIfNotExist();

            //q.Metadata.Add("HopHop", "1");
            //q.Metadata.Remove("HopHop");
            //q.SetMetadata();

            //CloudQueue q = Client.GetQueueReference("newqueue");
            //q.Clear();
            //q.Delete();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            var Client = storageAccount.CreateCloudQueueClient();
            CloudQueue q = Client.GetQueueReference("newqueue");
            CloudQueueMessage newMessage = new CloudQueueMessage(string.Format("text:{0}",TextBox1.Text));
            q.AddMessage(newMessage); //  TimeSpan.FromMinutes(1),TimeSpan.FromMinutes(2)
        }
    }
}