// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityAuthorize.cs" company="">
//   
// </copyright>
// <summary>
//   访问授权验证
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebAuth.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity.Owin;

    using WebAuth.Controllers;
    using WebAuth.Models;

    /// <summary>
    ///     访问授权验证
    /// </summary>
    [Description("验证访问权限")]
    public class IdentityAuthorizeAttribute : AuthorizeAttribute
    {
        #region Fields

        /// <summary>
        ///     授权上下文
        /// </summary>
        private AuthorizationContext _filterContext;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 重写授权验证方法
        /// </summary>
        /// <param name="filterContext">
        /// </param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            this._filterContext = filterContext;
            base.OnAuthorization(filterContext);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 重写核心验证方法
        /// </summary>
        /// <param name="httpContext">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // 取父类的验证结果
            bool result = base.AuthorizeCore(httpContext);

            // 如果验证未通过，则调用访问验证逻辑
            if (!result)
            {
                return this.HasPermission(this._filterContext);
            }

            return result;
        }

        /// <summary>
        /// 取当前用户的权限列表
        /// </summary>
        /// <param name="context">
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<ApplicationPermission> GetUserPermissions(HttpContextBase context)
        {
            // 取登录名
            string username = context.User.Identity.Name;

            // 构建缓存key
            string key = string.Format("UserPermissions_{0}", username);

            // 从缓存中取权限
            var permissions = HttpContext.Current.Session[key] as IEnumerable<ApplicationPermission>;

            // 若没有，则从db中取并写入缓存
            if (permissions == null)
            {
                // 取rolemanager
                var roleManager = context.GetOwinContext().Get<ApplicationRoleManager>();

                // 取用户权限集合
                permissions = roleManager.GetUserPermissions(username);

                // 写入缓存
                context.Session.Add(key, permissions);
            }

            return permissions;
        }

        /// <summary>
        /// 当前请求是否具有访问权限
        /// </summary>
        /// <param name="filterContext">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool HasPermission(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(filterContext.HttpContext.User.Identity.Name))
            {
                // if login then return false
                return false;
            }

            // 取当前用户的权限            
            IEnumerable<ApplicationPermission> rolePermissions = this.GetUserPermissions(filterContext.HttpContext);

            // 待访问的Action的Permission
            var action = new ApplicationPermission
                             {
                                 Action = filterContext.ActionDescriptor.ActionName, 
                                 Controller =
                                     filterContext.ActionDescriptor.ControllerDescriptor
                                     .ControllerName, 
                                 Description =
                                     ActionPermissionService.GetDescription(
                                         filterContext.ActionDescriptor)
                             };

            // 是否授权
            return rolePermissions.Contains(action, new ApplicationPermissionEqualityComparer());
        }

        #endregion
    }
}