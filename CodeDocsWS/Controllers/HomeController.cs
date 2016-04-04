using System;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using CodeDocsWS.Models;

namespace CodeDocsWS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Trace.TraceInformation("In Index function that returns Actionresult");
            return View();
        }

        public ActionResult About()
        {
            Trace.TraceInformation("In About funciton that returns ActionResult");
            string currentTime = System.DateTime.Now.ToLongTimeString();
            ViewBag.Message = "Your application description page. the time is: " + currentTime;

            return View();
        }

        public ActionResult Contact()
        {
            Trace.TraceInformation("In contact function that returns Actionresult");
            ViewBag.Message = "Your contact page.";
            
            return View();
        }

        [HttpPost]
        [ActionName("Contact")]
        public ActionResult ContactPost(HttpPostedFileBase image)        
        {
            Trace.TraceInformation("In contact post-function that uploads images");

            if (image != null)
            {
                try
                {

                    Guid id = Guid.NewGuid(); //id for new image uploaded
                    ViewBag.Guid = id.ToString();

                    string conStr = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;                    
                    ViewBag.ConnectionString = conStr;
                    
                    CloudStorageAccount storage = CloudStorageAccount.Parse(conStr);

                    CloudBlobClient blobClient = storage.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference("images");//has to be lower-case
                    container.CreateIfNotExists();
                    
                    CloudBlockBlob block = container.GetBlockBlobReference(id.ToString());
                    block.UploadFromStream(image.InputStream);

                    //A blob is uniquely identified by it's url so we will store a guid and a url in an azure table
                    CloudTableClient tableCLient = storage.CreateCloudTableClient();
                    CloudTable table = tableCLient.GetTableReference("images");
                    table.CreateIfNotExists();

                    ImageDetails image_details = new ImageDetails(id, block.Uri.ToString());
                    TableOperation insertOperation = TableOperation.Insert(image_details);

                    table.Execute(insertOperation);

                    ViewBag.Message = "Your image has been uploaded,....I think :).";
                }
                catch (Exception ex)
                {
                    ViewBag.message = ex.ToString();
                }

            }
            else
            {
                ViewBag.Message = "Your image probably wasnt uploaded :(";
            }
            return View();
        }

        public ActionResult WarmUp()
        {
            Thread.Sleep(30000); //sleep for thirty seconds

            return View();
        }
    }
}