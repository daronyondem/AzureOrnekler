using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BlobSample
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("files");
            container.CreateIfNotExists();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("files");
            var blob = container.GetBlockBlobReference(FileUpload1.FileName);
            blob.UploadFromStream(FileUpload1.FileContent);
            Response.Write(blob.Uri.ToString());
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("files");
            BlobContainerPermissions containerPermissions = new BlobContainerPermissions();
            containerPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
            container.SetPermissions(containerPermissions);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("files");

            foreach (var item in container.ListBlobs())
            {
                ((ICloudBlob)item).Delete();
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("files");
            foreach (var blobItem in container.ListBlobs())
            {
                using (var fileStream = System.IO.File.OpenWrite(Path.GetTempFileName()))
                {
                    ((ICloudBlob)blobItem).DownloadToStream(fileStream);
                }
            }
        }
    }
}