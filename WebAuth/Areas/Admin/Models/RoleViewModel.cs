// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The role view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Models
{
    using Resources.Resources.App_LocalResources;
using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     The role view model.
    /// </summary>
    public class RoleViewModel
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = AdminResource.Names.RoleViewModel_Name, ResourceType = typeof(AdminResource))]
        public string Name { get; set; }

        #endregion
    }
}