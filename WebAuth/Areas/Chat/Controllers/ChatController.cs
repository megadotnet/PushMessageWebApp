using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAuth.Controllers;
using WebAuth.Models;

namespace WebAuth.Areas.Chat.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {

        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }
    }
}