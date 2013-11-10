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
    public partial class paged : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            var productContext = new ProductContext();
            var CloudQuery = productContext.Products.AsTableServiceQuery();
            IAsyncResult iAsyncResult = CloudQuery.BeginExecuteSegmented(BeginExecuteSegmentedIsDone, CloudQuery); 
        }

        static void BeginExecuteSegmentedIsDone(IAsyncResult result)
        {
            CloudTableQuery<Entities.Product> CloudQuery = result.AsyncState as CloudTableQuery<Entities.Product>;
            var resultSegment = CloudQuery.EndExecuteSegmented(result);

            List<Entities.Product> listSongs = resultSegment.Results.ToList<Entities.Product>();

            if (resultSegment.HasMoreResults)
            {
                IAsyncResult iAsyncResult = CloudQuery.BeginExecuteSegmented(resultSegment.ContinuationToken, BeginExecuteSegmentedIsDone, CloudQuery); 
            }
        } 
    }
}