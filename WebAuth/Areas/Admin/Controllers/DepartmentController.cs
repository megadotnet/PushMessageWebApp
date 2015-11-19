// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DepartmentController.cs" company="">
//   
// </copyright>
// <summary>
//   The department controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebAuth.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using AutoMapper;

    using WebAuth.Controllers;
    using WebAuth.Models;

    using Webdiyer.WebControls.Mvc;

    /// <summary>
    /// The department controller.
    /// </summary>
    public partial class DepartmentController : BaseAdminController
    {

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [Description("新建部门")]
        public virtual ActionResult Create()
        {
            return this.View();
        }

        // POST: /Department/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="department">
        /// The department.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Description("新建部门，保存")]
        public virtual async Task<ActionResult> Create([Bind(Include = "ID,Name")] Department department)
        {
            if (this.ModelState.IsValid)
            {
                this._db.Departments.Add(department);
                await this._db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            var view = Mapper.Map<DepartmentViewModel>(department);
            return View(view);
        }

        // GET: /Department/Edit/5

        // GET: /Department/Delete/5
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("删除部门")]
        public virtual async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Department department = await this._db.Departments.FindAsync(id);
            if (department == null)
            {
                return this.HttpNotFound();
            }

            var view = Mapper.Map<DepartmentViewModel>(department);
            return View(view);
        }

        // POST: /Department/Delete/5
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
        [Description("删除部门，保存")]
        public virtual async Task<ActionResult> DeleteConfirmed(int id)
        {
            Department department = await this._db.Departments.FindAsync(id);
            this._db.Departments.Remove(department);
            await this._db.SaveChangesAsync();
            return this.RedirectToAction("Index");
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
        [Description("部门详情")]
        public virtual async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Department department = await this._db.Departments.FindAsync(id);
            if (department == null)
            {
                return this.HttpNotFound();
            }

            var view = Mapper.Map<DepartmentViewModel>(department);
            return View(view);
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
        [Description("编辑部门")]
        public virtual async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Department department = await this._db.Departments.FindAsync(id);
            if (department == null)
            {
                return this.HttpNotFound();
            }

            var view = Mapper.Map<DepartmentViewModel>(department);
            return View(view);
        }

        // POST: /Department/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="department">
        /// The department.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Description("编辑部门，保存")]
        public virtual async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Department department)
        {
            if (this.ModelState.IsValid)
            {
                this._db.Entry(department).State = EntityState.Modified;
                await this._db.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            var view = Mapper.Map<DepartmentViewModel>(department);
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
        [Description("部门列表")]
        public virtual async Task<ActionResult> Index(int index = 1)
        {
            List<Department> departments = await this._db.Departments.ToListAsync();
            var views = Mapper.Map<IList<DepartmentViewModel>>(departments);

            // 分页
            return View(views.ToPagedList(index, 10));
        }

        #endregion
    }
}