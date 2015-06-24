// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The permission view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Models
{
    using Resources.App_LocalResources;
    using System.ComponentModel.DataAnnotations;


    /// <summary>
    ///     The permission view model.
    /// </summary>
    public class PermissionViewModel
    {
        #region Public Properties

        /// <summary>
        ///     方法名
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name="Action", ResourceType = typeof(Resource))]   
        public string Action { get; set; }

        /// <summary>
        ///     控制器名
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Controller", ResourceType = typeof(Resource))]
        public string Controller { get; set; }

        /// <summary>
        ///     功能描述
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "PermissionViewModel_Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        /// <summary>
        ///     主键
        /// </summary>
        [Display(Name = "权限ID")]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the role id.
        /// </summary>
        [Display(Name = "角色ID")]
        public string RoleId { get; set; }

        /// <summary>
        ///     Gets or sets the role name.
        /// </summary>
        [Display(Name = "角色名")]
        public string RoleName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether selected.
        /// </summary>
        [Display(Name = "选择")]
        public bool Selected { get; set; }

        #endregion
    }
}