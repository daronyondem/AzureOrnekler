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
    public partial class insert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Test bulk insert
            //for (int i = 0; i < 3000; i++)
            //{
            //    var productContext = new ProductContext();
            //    var newProduct = new Entities.Product
            //    {
            //        PartitionKey = "Customer1",
            //        RowKey = "Product" + i.ToString(),
            //        Name = "Test" + i.ToString(),
            //        Description = "Test"
            //    };
            //    productContext.AddObject("Urunler", newProduct);
            //    productContext.SaveChanges();
            //}
           //Multiple rel query.
            //var productDetailContext = new ProductDetailContext();
            //var productContext = new ProductContext();

            //var results = (from g in this.context.RawEntity.Execute()
            //               where g.PartitionKey == site
            //               select g).ToList();
            //return results;



            //Rel SELECT
            //var ProductKeyToSearch = "Product1";
            //var MainProduct = productContext.Products.AsTableServiceQuery()
            //.Where(c => c.RowKey == ProductKeyToSearch && c.PartitionKey == "Customer1");
            //var Sonuc = productDetailContext.ProductDetails.AsTableServiceQuery()
            //.Where(c => c.RowKey == string.Format("detail_{0}", ProductKeyToSearch) && c.PartitionKey == "Customer1");



            //var productContext = new ProductContext();
            //var newProduct = new Entities.Product
            //{
            //    PartitionKey = "Customer1",
            //    RowKey = "Product1",
            //    Name = "Test",
            //    Description = "Test"
            //};
            //productContext.AddObject("Products", newProduct);
            //productContext.SaveChanges();

            ////Diff schema insert
            //var productDetailContext = new ProductDetailContext();
            //var newDetail = new Entities.ProductDetail
            //{
            //    DetailText = "Test",
            //    PartitionKey = "Customer1",
            //    RowKey = "detail_Product1"
            //};
            //productDetailContext.AddObject("Products", newDetail);
            //productDetailContext.SaveChanges();

////DELETE
            //var productContext = new ProductContext();
            //var entity = (from item in productContext.Products
            //              where item.PartitionKey == "Customer1" &&
            //              item.RowKey == "1"
            //              select item).First();
            //productContext.DeleteObject(entity);
            //productContext.SaveChanges();

            //var productContext = new ProductContext();
            //productContext.DeleteObject
            //(
            //    new WebRole1.Entities.ProductKey
            //    {
            //        PartitionKey = "Customer1",
            //        RowKey = "1"
            //    }
            //);

            ////productContext.SaveChanges();

////UPDATE
            //var productContext = new ProductContext();
            //var productUpdate = (from item in productContext.Products
            //                  where item.PartitionKey == "Customer1"
            //                  && item.RowKey == "1"
            //                  select item).First();
            //productUpdate.Description = "Changed";
            //productContext.UpdateObject(productUpdate);
            //productContext.SaveChanges();

            productContext.SaveChangesDefaultOptions =
    System.Data.Services.Client.SaveChangesOptions.Batch;

//Max 100 operations
//Max 4MB
//Same partition

            productContext.RetryPolicy = RetryPolicies.Retry(5, TimeSpan.FromSeconds(1));
        }
    }
}