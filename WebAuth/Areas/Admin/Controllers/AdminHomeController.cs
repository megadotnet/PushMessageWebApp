using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAuth.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: SuperAdmin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}