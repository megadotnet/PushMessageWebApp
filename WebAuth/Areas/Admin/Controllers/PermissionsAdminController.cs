// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionsAdminController.cs" company="">
//   
// </copyright>
// <summary>
//   The permissions admin controller.
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

    using Newtonsoft.Json;

    using WebAuth.Controllers;
    using WebAuth.Models;

    using Webdiyer.WebControls.Mvc;

    /// <summary>
    /// The permissions admin controller.
    /// </summary>
    public class PermissionsAdminController : BaseAdminController
    {
        // GET: PermissionsAdmin/Create
        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("新建权限，列表")]
        [GridDataSourceAction]
        public ActionResult Create()
        {
            // 创建ViewModel
            var permissionViews = new List<PermissionViewModel>();

            // 取程序集中权限
            IEnumerable<ApplicationPermission> allPermissions = this._permissionsOfAssembly;

            // 取数据库已有权限
            List<ApplicationPermission> dbPermissions = this._db.Permissions.ToList();

            // 取两者差集
            IEnumerable<ApplicationPermission> permissions = allPermissions.Except(
                dbPermissions, 
                new ApplicationPermissionEqualityComparer());
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

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("新建权限，保存")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IEnumerable<PermissionViewModel> data)
        {
            if (data == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "参数不能为空");
            }

            foreach (PermissionViewModel item in data)
            {
                // 创建权限
                var permission = Mapper.Map<ApplicationPermission>(item);
                this._db.Permissions.Add(permission);
            }

            // 保存
            await this._db.SaveChangesAsync();

            // 方法2，使用Newtonsoft.Json序列化结果对象
            // 格式为json字符串，客户端需要解析，即反序列化
            string result = JsonConvert.SerializeObject(new { Success = true });
            return new JsonResult { Data = result };
        }

        // GET: PermissionsAdmin/Edit/5

        // GET: PermissionsAdmin/Delete/5
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("删除权限")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationPermission applicationPermission = this._db.Permissions.Find(id);
            if (applicationPermission == null)
            {
                return this.HttpNotFound();
            }

            var view = Mapper.Map<PermissionViewModel>(applicationPermission);
            return View(view);
        }

        // POST: PermissionsAdmin/Delete/5
        /// <summary>
        /// The delete confirmed.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("删除权限，保存")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationPermission applicationPermission = this._db.Permissions.Find(id);
            this._db.Permissions.Remove(applicationPermission);
            this._db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// The details.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("权限详情")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationPermission applicationPermission = this._db.Permissions.Find(id);
            if (applicationPermission == null)
            {
                return this.HttpNotFound();
            }

            var view = Mapper.Map<PermissionViewModel>(applicationPermission);
            return View(view);
        }

        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("编辑权限")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationPermission applicationPermission = this._db.Permissions.Find(id);
            if (applicationPermission == null)
            {
                return this.HttpNotFound();
            }

            var view = Mapper.Map<PermissionViewModel>(applicationPermission);
            return View(view);
        }

        // POST: PermissionsAdmin/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("编辑权限，保存")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Controller,Action,Description")] PermissionViewModel view)
        {
            if (this.ModelState.IsValid)
            {
                // 对象映射
                var model = Mapper.Map<ApplicationPermission>(view);

                // 修改实体状态
                this._db.Entry(model).State = EntityState.Modified;
                this._db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return View(view);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("权限列表")]
        public async Task<ActionResult> Index(int index = 1)
        {
            List<ApplicationPermission> permissions = await this._db.Permissions.ToListAsync();

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
            return View(permissionViews.ToPagedList(index, 10));
        }

        #endregion
    }
}