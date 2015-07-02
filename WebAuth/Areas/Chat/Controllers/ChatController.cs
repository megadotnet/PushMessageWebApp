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
        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; private set; }


        /// <summary>
        /// Gets the context.
        /// </summary>
        public ApplicationDbContext context { get; private set; }

        public ChatController()
        {
            this.context = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.context));
        }

        // GET: Chat
        public async Task<ActionResult> Index()
        {
            ApplicationUser user = await this.UserManager.FindByEmailAsync(User.Identity.GetUserName());

            //trim mail address from orginal username
            if (user != null)
            {
                string tempusername = user.UserName;
                user.UserName = tempusername.Substring(0, tempusername.IndexOf('@'));
            }
            
            return View(user);
        }
    }
}