using BotDetect.Web.UI.Mvc;
using Elmah;
using Messag.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAuth.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    public partial class HomeController : BaseController
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly ILogger log = new Logger("HomeController");

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            log.DebugFormat("Index {0}", DateTime.Now);
            return View();
        }


        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns></returns>
        /// <see cref="http://www.c-sharpcorner.com/UploadFile/abhikumarvatsa/avoiding-cross-site-scripting-xss-attacks-with-antixss-in/"/>
        [ValidateInput(false)]
        public virtual ActionResult About()
        {
            ViewBag.Message = "alert('Message Center');";
           
            return View();
        }

        /// <summary>
        /// Contacts this instance.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Contact us";

            //http://blog.elmah.io/how-to-log-errors-to-elmah-programmatically/
            //http://blog.elmah.io/elmah-tutorial/
            //try
            //{
            //    int i = 0;
            //    int result = 42 / i;
            //}
            //catch (DivideByZeroException e)
            //{
            //    ErrorSignal.FromCurrentContext().Raise(e);
 
            //}

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Incorrect CAPTCHA code!")]
        public virtual ActionResult Contact(string model)
        {
            if (ModelState.IsValid)
            {
                // You should call ResetCaptcha as demonstrated in the following example.
                MvcCaptcha.ResetCaptcha("SampleCaptcha");
            }
            return Json(model);
        }

        /// <summary>
        /// Errors the log.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult ErrorLog()
        {
            return View();
        }
    }

   
}