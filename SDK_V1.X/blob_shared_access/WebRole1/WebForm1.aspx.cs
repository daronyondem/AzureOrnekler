using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
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

            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("dataConnStr");
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("testing12");
            container.CreateIfNotExist();

            CloudBlob blob = container.GetBlobReference("file.x");

            SharedAccessPolicy containeraccess = new SharedAccessPolicy();
            containeraccess.Permissions = SharedAccessPermissions.Read;

            SharedAccessPolicy blobaccess = new SharedAccessPolicy();
            blobaccess.SharedAccessExpiryTime =DateTime.Now.AddDays(1);

            BlobContainerPermissions perm = new BlobContainerPermissions();
            //perm.PublicAccess = BlobContainerPublicAccessType.Blob;
            perm.SharedAccessPolicies.Clear();
            perm.SharedAccessPolicies.Add("test", containeraccess);
            
            container.SetPermissions(perm, new BlobRequestOptions());

            HyperLink1.NavigateUrl =  blob.Uri.AbsoluteUri + 
                blob.GetSharedAccessSignature(blobaccess, "test");
        }
    }
}