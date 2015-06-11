using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuth.Models
{   
    
    /// <summary>
    /// 机构
    /// </summary>
    public class Department
    {
        public Department()
        {
            this.Users = new List<UserDepartment>();
        }
        /// <summary>
        /// 机构编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 下辖用户列表
        /// </summary>
        public virtual ICollection<UserDepartment> Users { get; set; }
    }

    public class UserDepartment
    {
        public int DepartmentId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
