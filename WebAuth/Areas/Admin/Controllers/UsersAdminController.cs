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

    /// <summary>
    /// The users admin controller.
    /// </summary>
    public class UsersAdminController : BaseAdminController
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
        public async Task<ActionResult> Create()
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
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string RoleId)
        {
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = userViewModel.UserName;

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

                return this.RedirectToAction("Index");
            }

            this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");
            return this.View();
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
        public async Task<ActionResult> Delete(string id)
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
        public async Task<ActionResult> DeleteConfirmed(string id)
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
        public async Task<ActionResult> Details(string id)
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
        public async Task<ActionResult> Edit(string id)
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
        public async Task<ActionResult> Edit(
            [Bind(Include = "UserName,Id,HomeTown")] ApplicationUser formuser, 
            string id, 
            string RoleId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");
            ApplicationUser user = await this.UserManager.FindByIdAsync(id);
            user.UserName = formuser.UserName;

            if (this.ModelState.IsValid)
            {
                // Update the user details
                await this.UserManager.UpdateAsync(user);

                // If user has existing Role then remove the user from the role
                // This also accounts for the case when the Admin selected Empty from the drop-down and
                // this means that all roles for the user must be removed
                IList<string> rolesForUser = await this.UserManager.GetRolesAsync(id);
                if (rolesForUser.Count() > 0)
                {
                    foreach (string item in rolesForUser)
                    {
                        IdentityResult result = await this.UserManager.RemoveFromRoleAsync(id, item);
                    }
                }

                if (!string.IsNullOrEmpty(RoleId))
                {
                    // Find Role
                    IdentityRole role = await this.RoleManager.FindByIdAsync(RoleId);

                    // Add user to new role
                    IdentityResult result = await this.UserManager.AddToRoleAsync(id, role.Name);
                    if (!result.Succeeded)
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.First());
                        this.ViewBag.RoleId = new SelectList(this.RoleManager.Roles, "Id", "Name");
                        return this.View();
                    }
                }

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
        public async Task<ActionResult> Index()
        {
            return this.View(await this.UserManager.Users.ToListAsync());
        }

        #endregion
    }
}