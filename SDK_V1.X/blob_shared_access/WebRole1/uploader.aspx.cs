using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace WebRole1
{
    public partial class uploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("dataConnStr");
            //CloudBlobClient blobClient = account.CreateCloudBlobClient();
            //CloudBlobContainer container = blobClient.GetContainerReference("testing");
            //container.CreateIfNotExist();
            //var blob = container.GetBlobReference("file.x");
            //Response.Write(blob.Uri.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("dataConnStr");
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("testing12");
            container.CreateIfNotExist();

            BlobContainerPermissions containerPermissions = new BlobContainerPermissions();
            containerPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
            container.SetPermissions(containerPermissions);
                       
            var blob = container.GetBlobReference("file.x");

            blob.UploadFromStream(FileUpload1.FileContent);

            Response.Write(blob.Uri.ToString());

            //using (FileStream fs = File.OpenRead(path))
            //{
            //    // Create the Blob and upload the file
            //    var blob = _BlobContainer.GetBlobReference(Guid.NewGuid().ToString() + "/" + filename);
            //    blob.UploadFromStream(fs);

            //    // Set the metadata into the blob
            //    blob.Metadata["FileName"] = filename;
            //    blob.Metadata["Submitter"] = "Automated Encoder";
            //    blob.SetMetadata();

            //    // Set the properties
            //    blob.Properties.ContentType = "video/x-ms-wmv";
            //    blob.SetProperties();
            //}
        }
    }
}