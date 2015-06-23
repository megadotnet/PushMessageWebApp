// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RolesAdminController.cs" company="">
//   
// </copyright>
// <summary>
//   The roles admin controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebAuth.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using WebAuth.Controllers;
    using WebAuth.Models;

    /// <summary>
    /// The roles admin controller.
    /// </summary>
    public class RolesAdminController : BaseAdminController
    {
        #region Fields

        /// <summary>
        /// The _role manager.
        /// </summary>
        private ApplicationRoleManager _roleManager;

        /// <summary>
        /// The _user manager.
        /// </summary>
        private ApplicationUserManager _userManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesAdminController"/> class.
        /// </summary>
        public RolesAdminController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesAdminController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="roleManager">
        /// The role manager.
        /// </param>
        public RolesAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the role manager.
        /// </summary>
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this._roleManager ?? this.HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }

            private set
            {
                this._roleManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the user manager.
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return this._userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            set
            {
                this._userManager = value;
            }
        }

        #endregion

        // 读取角色创建
        // GET: /Roles/Create
        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("Create Role WebPage")]
        public ActionResult Create()
        {
            return this.View();
        }

        // 异步写入角色创建
        // POST: /Roles/Create
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="roleViewModel">
        /// The role view model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [Description("Create Role action")]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var role = new ApplicationRole(roleViewModel.Name);
                IdentityResult roleresult = await this.RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    this.ModelState.AddModelError(string.Empty, roleresult.Errors.First());
                    return this.View();
                }

                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        // 异步读取角色编辑
        // GET: /Roles/Edit/Admin

        // 异步读取角色删除
        // GET: /Roles/Delete/5
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("Delete Role Page")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationRole role = await this.RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return this.HttpNotFound();
            }

            return View(role);
        }

        // 异步写入角色删除
        // POST: /Roles/Delete/5
        /// <summary>
        /// The delete confirmed.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="deleteUser">
        /// The delete user.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (this.ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ApplicationRole role = await this.RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return this.HttpNotFound();
                }

                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await this.RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await this.RoleManager.DeleteAsync(role);
                }

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
        [Description("Role Detail")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationRole role = await this.RoleManager.FindByIdAsync(id);

            // 读取角色内的用户列表。
            var users = new List<ApplicationUser>();
            foreach (ApplicationUser user in this.UserManager.Users.ToList())
            {
                if (await this.UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            this.ViewBag.Users = users;
            this.ViewBag.UserCount = users.Count();
            return View(role);
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
        [Description("Edit Role Page")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationRole role = await this.RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return this.HttpNotFound();
            }

            var roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        // 异步写入角色编辑
        // POST: /Roles/Edit/5
        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="roleModel">
        /// The role model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] RoleViewModel roleModel)
        {
            if (this.ModelState.IsValid)
            {
                ApplicationRole role = await this.RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                await this.RoleManager.UpdateAsync(role);
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("Roles List")]
        public ActionResult Index()
        {
            return this.View(this.RoleManager.Roles); // 显示角色清单
        }

        #endregion
    }
}