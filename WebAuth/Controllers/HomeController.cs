using Elmah;
using Messag.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAuth.Controllers
{
    public class HomeController : BaseController
    {
        private ILogger log = new Logger("HomeController");

        public ActionResult Index()
        {
            log.DebugFormat("Index {0}", DateTime.Now);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Message Center";
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us";

            //http://blog.elmah.io/how-to-log-errors-to-elmah-programmatically/
            //http://blog.elmah.io/elmah-tutorial/
            try
            {
                int i = 0;
                int result = 42 / i;
            }
            catch (DivideByZeroException e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
 
            }

            return View();
        }

        /// <summary>
        /// Errors the log.
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorLog()
        {
            return View();
        }
    }

   
}