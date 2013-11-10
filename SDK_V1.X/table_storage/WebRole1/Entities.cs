using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services.Common;
using Microsoft.WindowsAzure.StorageClient;

namespace WebRole1
{
    public class Entities
    {
        [DataServiceKey("PartitionKey", "RowKey")]
        public class Product2
        {
            public string Timestamp { get; set; }
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Product : TableServiceEntity
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class ProductKey : TableServiceEntity
        {

        }

        public class ProductDetail : TableServiceEntity
        {
            public string DetailText { get; set; }
        }
    }
}