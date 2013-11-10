using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using System.Data.Services.Client;

namespace WebRole1
{
    public class ProductDetailContext : TableServiceContext
    {
        private static CloudStorageAccount storageAccount =
        CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

        public ProductDetailContext()
            : base(storageAccount.TableEndpoint.AbsoluteUri, storageAccount.Credentials)
        {
        }

        public DataServiceQuery<WebRole1.Entities.ProductDetail> ProductDetails
        {
            get
            {
                return CreateQuery<WebRole1.Entities.ProductDetail>("Products");
            }
        }
    }
}