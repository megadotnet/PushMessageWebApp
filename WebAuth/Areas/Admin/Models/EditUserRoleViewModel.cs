// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditUserRoleViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The edit user role view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    ///     The edit user role view model.
    /// </summary>
    public class EditUserRoleViewModel
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "电邮地址")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the roles list.
        /// </summary>
        public IEnumerable<SelectListItem> RolesList { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        [Display(Name = "用户名")]
        [Required]
        public string UserName { get; set; }

        #endregion
    }
}