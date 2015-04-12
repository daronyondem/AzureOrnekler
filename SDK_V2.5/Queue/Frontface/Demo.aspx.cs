using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Frontface
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudQueueClient queueClient = account.CreateCloudQueueClient();
            CloudQueue q = queueClient.GetQueueReference("jobqueue");
            await q.CreateIfNotExistsAsync();
        }

    }
}
