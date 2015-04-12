﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Urunler");
            await table.CreateIfNotExistsAsync();
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Urunler");
            Urun urun = new Urun()
            {
                PartitionKey = "Musteri1",
                RowKey = (new Random().Next(1, 100)).ToString(),
                Adi = "Deneme",
                Aciklama = "Açıklama"
            };
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(urun);
            TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
        }
    }
}