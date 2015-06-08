using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace WebAuth.Models
{
    public class MySqlInitializer : IDatabaseInitializer<ApplicationDbContext>
    {
        public void InitializeDatabase(ApplicationDbContext context)
        {
            if (!context.Database.Exists())
            {
                // if database did not exist before - create it
                context.Database.Create();


                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                string adminUsername = "admin@163.com";
                string adminRoleName = "Administrators";
                string adminPassword = "admin123";
                string commonRole = "Users";

                //Create Role Test and User Test
                  var commonRoleResults=RoleManager.Create(new IdentityRole(commonRole));
                  var defaultTestUser = new ApplicationUser() { UserName = "peter@163.com", Email = "peter@163.com" };
                  var userResults = UserManager.Create(defaultTestUser,"123456");
                if (commonRoleResults.Succeeded)
                {
                    UserManager.AddToRole(defaultTestUser.Id, commonRole);
                }
                  

                //Create Role Admin if it does not exist
                if (!RoleManager.RoleExists(adminRoleName))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(adminRoleName));
                }

                //Create User=admin@163.com with password=admin123
                var user = new ApplicationUser()
                {
                    UserName = adminUsername,
                    Email = adminUsername,
                };
          
                var adminresult = UserManager.Create(user, adminPassword);

                //Add User Admin to Role Admin
                if (adminresult.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, adminRoleName);
                }
            }
        }
    }
}