// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationRole.cs" company="">
//   
// </copyright>
// <summary>
//   The application role.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNet.Identity.EntityFramework;
using Resources.App_LocalResources;

    /// <summary>
    ///     The application role.
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationRole" /> class.
        /// </summary>
        public ApplicationRole()
        {
            this.Permissions = new List<ApplicationRolePermission>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        public ApplicationRole(string roleName)
            : this()
        {
            base.Name = roleName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        [Display(Name = "ApplicationRole_Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        /// <summary>
        ///     权限列表
        /// </summary>
        public ICollection<ApplicationRolePermission> Permissions { get; set; }

        #endregion
    }
}