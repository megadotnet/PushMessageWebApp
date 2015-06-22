// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationRolePermission.cs" company="">
//   
// </copyright>
// <summary>
//   The application role permission.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     The application role permission.
    /// </summary>
    public class ApplicationRolePermission
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the permission id.
        /// </summary>
        public virtual string PermissionId { get; set; }

        /// <summary>
        ///     Gets or sets the role id.
        /// </summary>
        public virtual string RoleId { get; set; }

        #endregion
    }

    /// <summary>
    ///     The application permission equality comparer.
    /// </summary>
    public class ApplicationPermissionEqualityComparer : IEqualityComparer<ApplicationPermission>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(ApplicationPermission x, ApplicationPermission y)
        {
            // 先比较ID
            if (string.Compare(x.Id, y.Id, true) == 0)
            {
                return true;
            }

            // 而后比较Controller,Action,Description和Params
            if (x.Controller == y.Controller && x.Action == y.Action && x.Description == y.Description)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetHashCode(ApplicationPermission obj)
        {
            string str = string.Format("{0}-{1}-{2}", obj.Controller, obj.Action, obj.Description);
            return str.GetHashCode();
        }

        #endregion
    }
}