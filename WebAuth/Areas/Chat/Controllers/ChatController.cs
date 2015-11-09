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
    /// <summary>
    /// ChatController
    /// </summary>
    [Authorize]
    public partial class ChatController : BaseController
    {

        // GET: Chat
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}