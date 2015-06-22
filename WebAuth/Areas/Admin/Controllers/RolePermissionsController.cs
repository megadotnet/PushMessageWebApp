// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RolePermissionsController.cs" company="">
//   
// </copyright>
// <summary>
//   The role permissions controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebAuth.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using AutoMapper;

    using Infragistics.Web.Mvc;

    using WebAuth.Controllers;
    using WebAuth.Models;

    using Webdiyer.WebControls.Mvc;

    /// <summary>
    /// The role permissions controller.
    /// </summary>
    public class RolePermissionsController : BaseController
    {
        // GET: RolePermissions

        // POST: RolePermissions/Create
        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("新建角色-权限,列表")]
        [GridDataSourceAction]
        public ActionResult Create(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<ApplicationRole> roles = this._roleManager.Roles.ToList();
            this.ViewBag.RoleID = new SelectList(roles, "ID", "Name", roleId);

            // 取角色权限ID
            IEnumerable<ApplicationPermission> rolePermissions = this._roleManager.GetRolePermissions(roleId);

            // 取全部权限与角色权限的差集
            List<ApplicationPermission> allPermission = this._db.Permissions.ToList();
            IEnumerable<ApplicationPermission> permissions = allPermission.Except(
                rolePermissions, 
                new ApplicationPermissionEqualityComparer());

            // 创建ViewModel
            var permissionViews = new List<PermissionViewModel>();
            permissions.Each(
                t =>
                    {
                        var view = Mapper.Map<PermissionViewModel>(t);

                        permissionViews.Add(view);
                    });

            // 排序
            permissionViews.Sort(new PermissionViewModelComparer());
            return View(permissionViews.AsQueryable());
        }

        // POST: RolePermissions/Edit/5
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("新建角色-权限，保存")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string roleId, IEnumerable<ApplicationPermission> data)
        {
            if (string.IsNullOrWhiteSpace(roleId) || data.Count() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // 添加Permission
            foreach (ApplicationPermission item in data)
            {
                var permission = new ApplicationRolePermission { RoleId = roleId, PermissionId = item.Id };

                // 方法1,用set<>().Add()
                this._db.Set<ApplicationRolePermission>().Add(permission);
            }

            // 保存;
            int records = await this._db.SaveChangesAsync();

            // 方法1，用JsonResult类封装，格式为Json，客户端直接使用
            var response = new Dictionary<string, bool>();
            response.Add("Success", true);
            return new JsonResult { Data = response };
        }

        // GET: RolePermissions/Delete/5
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        /// <param name="permissionId">
        /// The permission id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("删除角色-权限")]
        public async Task<ActionResult> Delete(string roleId, string permissionId)
        {
            if (string.IsNullOrWhiteSpace(roleId) || string.IsNullOrWhiteSpace(permissionId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationPermission applicationPermission = this._db.Permissions.Find(permissionId);
            ApplicationRole role = await this._roleManager.FindByIdAsync(roleId);
            if (applicationPermission == null)
            {
                return this.HttpNotFound();
            }

            // 创建viewModel
            var view = Mapper.Map<PermissionViewModel>(applicationPermission);
            view.RoleId = roleId;
            view.RoleName = role.Name;

            return View(view);
        }

        // POST: RolePermissions/Delete/5
        /// <summary>
        /// The delete confirmed.
        /// </summary>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        /// <param name="permissionId">
        /// The permission id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("删除角色-权限，保存")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string roleId, string permissionId)
        {
            if (string.IsNullOrWhiteSpace(roleId) || string.IsNullOrWhiteSpace(permissionId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (this.ModelState.IsValid)
            {
                // 验证role与permission
                ApplicationRole role = await this._roleManager.FindByIdAsync(roleId);
                ApplicationPermission permission = this._db.Permissions.Find(permissionId);
                if (role == null || permission == null)
                {
                    return this.HttpNotFound();
                }

                // 删除Permission
                var entity = new ApplicationRolePermission { RoleId = roleId, PermissionId = permissionId };
                this._db.Set<ApplicationRolePermission>().Attach(entity);
                this._db.Entry(entity).State = EntityState.Deleted;

                int result = await this._db.SaveChangesAsync();
            }

            return this.RedirectToAction("Index", new { roleId });
        }

        /// <summary>
        /// The details.
        /// </summary>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        /// <param name="permissionId">
        /// The permission id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("角色-权限详情")]
        public async Task<ActionResult> Details(string roleId, string permissionId)
        {
            if (string.IsNullOrWhiteSpace(roleId) || string.IsNullOrWhiteSpace(permissionId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // 取权限
            ApplicationPermission applicationPermission = this._db.Permissions.Find(permissionId);

            // 取角色
            ApplicationRole role = await this._roleManager.FindByIdAsync(roleId);
            if (applicationPermission == null)
            {
                return this.HttpNotFound();
            }

            var view = Mapper.Map<PermissionViewModel>(applicationPermission);
            view.RoleId = roleId;
            view.RoleName = role.Name;

            return View(view);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("角色-权限列表")]
        public ActionResult Index(string roleId, int index = 1)
        {
            // 取role列表
            List<ApplicationRole> roles = this._roleManager.Roles.ToList();

            // roleId是否为空
            if (string.IsNullOrWhiteSpace(roleId))
            {
                // 取第一个role的id
                roleId = roles.FirstOrDefault().Id;
            }

            // 放入viewbag，设置默认值
            this.ViewBag.RoleID = new SelectList(roles, "ID", "Name", roleId);

            // 取角色权限列表
            IEnumerable<ApplicationPermission> permissions = this._roleManager.GetRolePermissions(roleId);

            // 创建ViewModel
            var permissionViews = new List<PermissionViewModel>();

            // var map = Mapper.CreateMap<ApplicationPermission, PermissionViewModel>();
            permissions.Each(
                t =>
                    {
                        var view = Mapper.Map<PermissionViewModel>(t);
                        view.RoleId = roleId;
                        permissionViews.Add(view);
                    });

            // 排序
            permissionViews.Sort(new PermissionViewModelComparer());
            return View(permissionViews.ToPagedList(index, 10));
        }

        #endregion
    }
}