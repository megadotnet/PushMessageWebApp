// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DepartmentViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The department view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebAuth.Models
{
    using Resources.Resources.App_LocalResources;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     The department view model.
    /// </summary>
    public class DepartmentViewModel
    {
        #region Public Properties

        /// <summary>
        ///     机构编号
        /// </summary>
        [Required]
        [Display(Name = AdminResource.Names.DepartmentViewModelId, ResourceType = typeof(AdminResource))]
        public int Id { get; set; }

        /// <summary>
        ///     机构名称
        /// </summary>
        [Required]
        [Display(Name = AdminResource.Names.DepartmentViewModelName,ResourceType = typeof(AdminResource))]
        public string Name { get; set; }

        #endregion
    }
}