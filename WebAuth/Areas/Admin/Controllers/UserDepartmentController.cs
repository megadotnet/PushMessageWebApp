// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDepartmentController.cs" company="">
//   
// </copyright>
// <summary>
//   The user department controller.
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
    using AutoMapper.Internal;

    using WebAuth.Controllers;
    using WebAuth.Models;

    using Webdiyer.WebControls.Mvc;

    /// <summary>
    /// The user department controller.
    /// </summary>
    [Description("用户-部门维护")]
    public partial class UserDepartmentController : BaseAdminController
    {
        // GET: UsersAdmin

        // 异步读取用户详情
        // GET: /Users/Details/5
        #region Public Methods and Operators

        /// <summary>
        /// The details.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("用户-部门详情")]
        public virtual async Task<ActionResult> Details(string id)
        {
            // 用户为空时返回400错误
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // 按Id查找用户
            ApplicationUser user = await this._userManager.FindByIdAsync(id);
            var view = Mapper.Map<EditUserDepartmentViewModel>(user);
            IEnumerable<int> departmentIds = user.Departments.Select(d => d.DepartmentId);

            this.ViewBag.DepartmentNames = departmentIds.Select(t => this._db.Departments.Find(t).Name).ToList();

            return View(view);
        }

        // 读取用户编辑
        // GET: /Users/Edit/1
        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("编辑用户-部门")]
        public virtual async Task<ActionResult> Edit(string id)
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

            // 取部门名称
            List<int> departmentIds = user.Departments.Select(d => d.DepartmentId).ToList();

            var view = Mapper.Map<EditUserDepartmentViewModel>(user);

            // 部门是否选中
            view.DepartmentList =
                this._db.Departments.ToList()
                    .Select(
                        x =>
                        new SelectListItem
                            {
                                Selected = departmentIds.Contains(x.Id), 
                                Text = x.Name, 
                                Value = x.Id.ToString()
                            });
            return View(view);
        }

        // 写入用户编辑
        // POST: /Users/Edit/5
        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="editUser">
        /// The edit user.
        /// </param>
        /// <param name="selectedDepartments">
        /// The selected departments.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Description("编辑用户-部门，保存")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(
            [Bind(Include = "UserName,Email,Id")] EditUserDepartmentViewModel editUser, 
            params int[] selectedDepartments)
        {
            if (this.ModelState.IsValid)
            {
                ApplicationUser user = await this._userManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return this.HttpNotFound();
                }

                // 删除现在所属部门
                List<UserDepartment> departments = user.Departments.ToList();
                departments.Each(t => this._db.Set<UserDepartment>().Remove(t));

                // 新增所属部门
                selectedDepartments = selectedDepartments ?? new int[] { };
                selectedDepartments.Each(
                    departmentId =>
                        {
                            var userDepartment = new UserDepartment
                                                     {
                                                         ApplicationUserId = user.Id, 
                                                         DepartmentId = departmentId
                                                     };
                            this._db.Set<UserDepartment>().Add(userDepartment);
                        });

                this._db.SaveChanges();

                return this.RedirectToAction("Index");
            }

            this.ModelState.AddModelError(string.Empty, "操作失败。");
            return this.View();
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
        [Description("用户-部门列表")]
        public virtual async Task<ActionResult> Index(int index = 1)
        {
            List<ApplicationUser> users = await this._userManager.Users.ToListAsync();
            var views = Mapper.Map<IList<EditUserDepartmentViewModel>>(users);

            return View(views.ToPagedList(index, 10));
        }

        #endregion
    }
}