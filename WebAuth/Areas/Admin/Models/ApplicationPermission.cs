// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationPermission.cs" company="Megadotnet">
//   Copyright @ PeterLiu 2015 
// </copyright>
// <summary>
//   The application permission.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The application permission.
    /// </summary>
    public class ApplicationPermission
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationPermission" /> class.
        /// </summary>
        public ApplicationPermission()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new List<ApplicationRolePermission>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     方法名
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        ///     控制器名
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        ///     功能描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     参数字符串
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        ///     角色列表
        /// </summary>
        public ICollection<ApplicationRolePermission> Roles { get; set; }

        #endregion
    }
}