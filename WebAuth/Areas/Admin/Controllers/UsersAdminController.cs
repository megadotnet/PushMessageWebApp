// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersAdminController.cs" company="">
//   
// </copyright>
// <summary>
//   The users admin controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebAuth.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using WebAuth.Controllers;
    using WebAuth.Models;
    using Microsoft.Security.Application;
    using System.IO;
    using System.Web;

    /// <summary>
    /// The users admin controller.
    /// </summary>
    public partial class UsersAdminController : BaseAdminController
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersAdminController"/> class.
        /// </summary>
        public UsersAdminController()
        {
            this.context = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.context));
            this.RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.context));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersAdminController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="roleManager">
        /// The role manager.
        /// </param>
        public UsersAdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the role manager.
        /// </summary>
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; private set; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        public ApplicationDbContext context { get; private set; }

        #endregion

        // GET: /Users/

        // GET: /Users/Create
        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("User Create Page")]
        public virtual async Task<ActionResult> Create()
        {
            // Get the list of Roles
            this.ViewBag.RoleId = new SelectList(await this.RoleManager.Roles.ToListAsync(), "Id", "Name");
            return this.View();
        }

        // POST: /Users/Create
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="userViewModel">
        /// The user view model.
        /// </param>
        /// <param name="RoleId">
        /// The role id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [Description("User Create Action")]
        public virtual async Task<ActionResult> Create(RegisterViewModel userViewModel, string RoleId)
        {
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser();
                
                user.UserName = Sanitizer.GetSafeHtmlFragment(userViewModel.UserName);

                IdentityResult adminresult = await this.UserManager.CreateAsync(user, userViewModel.Password);

                // Add User Admin to Role Admin
                if (adminresult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(RoleId))
                    {
                        // Find Role Admin
                        IdentityRole role = await this.RoleManager.FindByIdAsync(RoleId);
                        IdentityResult result = await this.UserManager.AddToRoleAsync(user.Id, role.Name);
                        if (!result.Succeeded)
                        {
                            this.ModelState.AddModelError(string.Empty, result.Errors.First());
                            this.ViewBag.RoleId = new SelectList(
                                await this.RoleManager.Roles.ToListAsync(), 
                                "Id", 
                                "Name");
                            return this.View();
                        }
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, adminresult.Errors.First());
                    this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");
                    return this.View();
                }
                //force clear caching
                ClearCache();

                return this.RedirectToAction("Index");
            }

            this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");
            return this.View();
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        private void ClearCache()
        {
            Response.RemoveOutputCacheItem(Url.Action("Index", "UsersAdmin"));
        }

        // GET: /Users/Edit/[GUID]

        ////
        //// GET: /Users/Delete/5
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("User Delete Page")]
        public virtual async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = await this.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            return View(user);
        }

        ////
        //// POST: /Users/Delete/5
        /// <summary>
        /// The delete confirmed.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Description("User DeleteConfirmed")]
        public virtual async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (this.ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ApplicationUser user = await this._userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return this.HttpNotFound();
                }

                IdentityResult result = await this._userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    this.ModelState.AddModelError(string.Empty, result.Errors.First());
                    return this.View();
                }

                //force clear caching
                ClearCache();

                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        /// <summary>
        /// The details.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("User Detail")]
        public virtual async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = await this.UserManager.FindByIdAsync(id);
            this.ViewBag.CurrentUserRoles = await this.UserManager.GetRolesAsync(id);
            return View(user);
        }

        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("User Edit Page")]
        public virtual async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IList<string> currentRoleId = await this.UserManager.GetRolesAsync(id);
            if (currentRoleId != null && currentRoleId.Count > 0)
            {
                IdentityRole rolesofUser = await this.RoleManager.FindByNameAsync(currentRoleId.FirstOrDefault());
                this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name", rolesofUser.Id);
            }
            else
            {
                this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");
            }

            ApplicationUser user = await this.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            return View(user);
        }

        // POST: /Users/Edit/5
        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="formuser">
        /// The formuser.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="RoleId">
        /// The role id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Description("User Edit Action")]
        public virtual async Task<ActionResult> Edit(ApplicationUser formuser,
            string RoleId, HttpPostedFileBase file)
        {
            if (formuser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");
            ApplicationUser user = await this.UserManager.FindByIdAsync(formuser.Id);
            user.UserName = formuser.UserName;
            user.ChineseName = formuser.ChineseName;
            user.Email = formuser.Email;

            if (file != null)
            {
                string folderpath = "~/Uploads";
                string dirpath = Server.MapPath(folderpath);
                if(!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                string filename = Path.GetFileName(file.FileName);
                string path = Path.Combine(dirpath, filename);
                file.SaveAs(path);
                user.HeaderPhoto = "/Uploads/" + filename;
            }

            //if (Request.Files.Count > 0)
            //{
            //    var file = Request.Files[0];

            //    if (file != null && file.ContentLength > 0)
            //    {
            //        var fileName = Path.GetFileName(file.FileName);
            //        var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
            //        file.SaveAs(path);
            //        user.HeaderPhoto = path;
            //    }
            //}

            if (this.ModelState.IsValid)
            {
                // Update the user details
                await this.UserManager.UpdateAsync(user);

                // If user has existing Role then remove the user from the role
                // This also accounts for the case when the Admin selected Empty from the drop-down and
                // this means that all roles for the user must be removed
                IList<string> rolesForUser = await this.UserManager.GetRolesAsync(formuser.Id);
                if (rolesForUser.Count() > 0)
                {
                    foreach (string item in rolesForUser)
                    {
                        IdentityResult result = await this.UserManager.RemoveFromRoleAsync(formuser.Id, item);
                    }
                }

                if (!string.IsNullOrEmpty(RoleId))
                {
                    // Find Role
                    IdentityRole role = await this.RoleManager.FindByIdAsync(RoleId);

                    // Add user to new role
                    IdentityResult result = await this.UserManager.AddToRoleAsync(formuser.Id, role.Name);
                    if (!result.Succeeded)
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.First());
                        this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");
                        return this.View();
                    }
                }
                //force clear caching
                ClearCache();
                return this.RedirectToAction("Index");
            }

            this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");

            return this.View();
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("Users List Page")]
        [OutputCache(VaryByParam = "none", Duration = 3600)]
        public virtual async Task<ActionResult> Index()
        {
            return this.View(await this.UserManager.Users.ToListAsync());
        }

        #endregion
    }
}