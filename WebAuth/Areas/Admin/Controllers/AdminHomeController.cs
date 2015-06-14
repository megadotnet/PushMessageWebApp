using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAuth.Controllers;

namespace WebAuth.Areas.Admin.Controllers
{
    public class AdminHomeController : BaseController
    {
        // GET: SuperAdmin/Home
         [Description("AdminHomeMainPage")]
        public ActionResult Index()
        {
            return View();
        }
    }
}