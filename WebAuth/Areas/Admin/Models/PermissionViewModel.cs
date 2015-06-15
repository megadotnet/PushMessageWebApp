using System;
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
}
