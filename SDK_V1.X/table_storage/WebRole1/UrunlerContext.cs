using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using System.Data.Services.Client;

namespace WebRole1
{
    public class ProductContext : TableServiceContext
    {
        private static CloudStorageAccount storageAccount =
        CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

        public ProductContext()
            : base(storageAccount.TableEndpoint.AbsoluteUri, storageAccount.Credentials)
        {
        }

        public DataServiceQuery<WebRole1.Entities.Product> Products
        {
            get
            {
                return CreateQuery<WebRole1.Entities.Product>("Products");
            }
        }
    }
}