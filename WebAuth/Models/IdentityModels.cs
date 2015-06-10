using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System;

namespace WebAuth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { this.Departments = new List<UserDepartment>(); }
        public ApplicationUser(string userName) : base(userName) { }
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual ICollection<UserDepartment> Departments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }



  
}