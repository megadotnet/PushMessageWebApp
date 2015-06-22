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
    ///     机构
    /// </summary>
    public class Department
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Department" /> class.
        /// </summary>
        public Department()
        {
            this.Users = new List<UserDepartment>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     机构编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     机构名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     下辖用户列表
        /// </summary>
        public virtual ICollection<UserDepartment> Users { get; set; }

        #endregion
    }

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