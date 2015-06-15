using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuth.Models
{
    public class ApplicationPermission
    {
        public ApplicationPermission()
        {
            Id = Guid.NewGuid().ToString();
            Roles = new List<ApplicationRolePermission>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 参数字符串
        /// </summary>
        public string Params { get; set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public ICollection<ApplicationRolePermission> Roles { get; set; }


    }
}
