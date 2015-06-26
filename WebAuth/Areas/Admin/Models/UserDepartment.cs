// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDepartment.cs" company="">
//   
// </copyright>
// <summary>
//   机构
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Models
{
    using System.Collections.Generic;


    /// <summary>
    ///     The user department.
    /// </summary>
    public class UserDepartment
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the application user id.
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the department id.
        /// </summary>
        public int DepartmentId { get; set; }

        #endregion
    }
}