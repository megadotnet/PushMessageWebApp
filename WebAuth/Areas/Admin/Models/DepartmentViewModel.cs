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
        public int Id { get; set; }

        /// <summary>
        ///     机构名称
        /// </summary>
        [Required]
        [Display(Name = "机构名称")]
        public string Name { get; set; }

        #endregion
    }
}