using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebAuth.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
            : base()
        {
            Permissions = new List<ApplicationRolePermission>();
        }
        public ApplicationRole(string roleName)
            : this()
        {
            base.Name = roleName;
        }

        [Display(Name = "角色描述")]
        public string Description { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        public ICollection<ApplicationRolePermission> Permissions { get; set; }
    }
}
