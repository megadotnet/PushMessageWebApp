using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebAuth.Models
{
    public class PermissionViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "权限ID")]
        public string Id { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "控制器名")]
        public string Controller { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "方法名")]
        public string Action { get; set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "功能描述")]
        public string Description { get; set; }
        [Display(Name = "选择")]
        public bool Selected { get; set; }
        [Display(Name = "角色ID")]
        public string RoleId { get; set; }
        [Display(Name = "角色名")]
        public string RoleName { get; set; }
    }

    public class PermissionViewModelComparer : IComparer<PermissionViewModel>
    {
        public int Compare(PermissionViewModel x, PermissionViewModel y)
        {
            //id相同，则相等
            if (string.Compare(x.Id, y.Id, true) == 0)
            {
                return 0;
            }
            //controller比较
            var controllerCompareResult = string.Compare(x.Controller, y.Controller, true);
            //action比较
            var actionCompareResult = string.Compare(x.Action, y.Action, true);
            //先比较controller,后比较action
            if (controllerCompareResult != 0)
            {
                return controllerCompareResult;
            }
            else
            {
                return actionCompareResult;
            }
        }
    }
}
