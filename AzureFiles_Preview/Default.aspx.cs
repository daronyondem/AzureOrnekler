using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private string connString = "DefaultEndpointsProtocol=http;AccountName=azurefilesdarontest;AccountKey=2Fm1k6KKtuP6BkWCpKOIPIn7dtcieHkYn55pW52bYoeJysh3IKxCK8asnv7thu3bKTk+dNwjkNpqPfHhAItw2Q==;";

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(this.connString);
        CloudFileClient client = account.CreateCloudFileClient();
        CloudFileShare share = client.GetShareReference("birklasor");
        share.CreateIfNotExistsAsync().Wait();       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Process p = new Process();
        int exitCode;
        p.StartInfo.FileName = "net.exe";
        p.StartInfo.Arguments = "use z: \\azurefilesdarontest.file.core.windows.net\birklasor /u:azurefilesdarontest 2Fm1k6KKtuP6BkWCpKOIPIn7dtcieHkYn55pW52bYoeJysh3IKxCK8asnv7thu3bKTk+dNwjkNpqPfHhAItw2Q==";
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardError = true;
        p.Start();
        string error = p.StandardError.ReadToEnd();
        p.WaitForExit(20000);
        exitCode = p.ExitCode;
        p.Close();

        using (StreamWriter outfile = new StreamWriter(@"Z:\deneme.txt"))
        {
            outfile.Write("Merhaba Dünya!");
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(this.connString);
        CloudFileClient client = account.CreateCloudFileClient();
        CloudFileShare share = client.GetShareReference("birklasor");
        CloudFileDirectory rootDirectory = share.GetRootDirectoryReference();
        CloudFile aCloudFile = rootDirectory.GetFileReference("deneme.txt");
        Response.Write(aCloudFile.DownloadText());
    }
}