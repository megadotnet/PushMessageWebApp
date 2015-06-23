using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Web;
using WebAuth.Models;
using WebAuth.Areas.Admin.Controllers;
using WebAuth.Helper;
using System.Threading;

namespace WebAuth.Controllers
{
    /// <summary>
    /// 基础类
    /// </summary>
    [IdentityAuthorize(Roles = "Administrators")]
    public abstract class BaseAdminController : BaseController
    {
        public BaseAdminController()
        {

        }
        public BaseAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private ApplicationUserManager userManager;
        protected ApplicationUserManager _userManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        private ApplicationRoleManager roleManager;
        protected ApplicationRoleManager _roleManager
        {
            get
            {
                return roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                roleManager = value;
            }
        }

        private ApplicationDbContext db;
        protected ApplicationDbContext _db
        {
            get
            {
                return db ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set
            {
                db = value;
            }
        }
        /// <summary>
        /// 缓存key
        /// </summary>
        private readonly static string _permissionKey = "PermissionsOfAssembly";
        /// <summary>
        /// 程序集中权限集合
        /// </summary>
        protected IEnumerable<ApplicationPermission> _permissionsOfAssembly
        {
            get
            {
                var permissions = HttpContext.Application.Get(_permissionKey) as IEnumerable<ApplicationPermission>;
                if (permissions == null)
                {
                    //取程序集中全部权限
                    permissions = ActionPermissionService.GetAllActionByAssembly();
                    //添加到缓存
                    HttpContext.Application.Add(_permissionKey, permissions);
                }
                return permissions;
            }
        }

    }
}