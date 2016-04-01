using System.Threading;
using System.Diagnostics;
using System.Web.Mvc;

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

        public ActionResult WarmUp()
        {
            Thread.Sleep(30000); //sleep for thirty seconds

            return View();
        }
    }
}