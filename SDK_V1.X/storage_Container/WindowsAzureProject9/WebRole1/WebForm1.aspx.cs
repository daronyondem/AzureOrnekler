using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WebRole1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
                {
                    configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));
                });
            };
            CloudStorageAccount account =CloudStorageAccount.FromConfigurationSetting("DataConnectionString"); 
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container =
            blobClient.GetContainerReference("ornekcontainer");
            container.Create();
        }
    }
}