using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebAuth.Models
{
    public class ApplicationRolePermission
    {
        public virtual string RoleId { get; set; }
        public virtual string PermissionId { get; set; }
    }

    public class ApplicationPermissionEqualityComparer : IEqualityComparer<ApplicationPermission>
    {
        public bool Equals(ApplicationPermission x, ApplicationPermission y)
        {
            //先比较ID
            if (string.Compare(x.Id, y.Id, true) == 0)
            {
                return true;
            }
            //而后比较Controller,Action,Description和Params
            if (x.Controller == y.Controller && x.Action == y.Action && x.Description == y.Description)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(ApplicationPermission obj)
        {
            var str = string.Format("{0}-{1}-{2}", obj.Controller, obj.Action, obj.Description);
            return str.GetHashCode();
        }
    }
}
