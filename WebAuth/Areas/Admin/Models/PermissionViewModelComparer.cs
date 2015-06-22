// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionViewModelComparer.cs" company="">
//   
// </copyright>
// <summary>
//   The permission view model comparer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     The permission view model comparer.
    /// </summary>
    public class PermissionViewModelComparer : IComparer<PermissionViewModel>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Compare(PermissionViewModel x, PermissionViewModel y)
        {
            // id相同，则相等
            if (string.Compare(x.Id, y.Id, true) == 0)
            {
                return 0;
            }

            // controller比较
            int controllerCompareResult = string.Compare(x.Controller, y.Controller, true);

            // action比较
            int actionCompareResult = string.Compare(x.Action, y.Action, true);

            // 先比较controller,后比较action
            if (controllerCompareResult != 0)
            {
                return controllerCompareResult;
            }

            return actionCompareResult;
        }

        #endregion
    }
}