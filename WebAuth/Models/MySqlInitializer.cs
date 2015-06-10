using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
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


    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //创建用户名为admin@163.com，密码为“admin123”并把该用户添加到角色组"Admin"中
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(db));
            string name1 = "admin@163.com";//用户名
             string email1 = "admin@163.com";//邮箱
             string password1 = "admin123";//密码
             string roleName1 = "Administrators";//用户要添加到的角色组
             string name2 = "peter@163.com";//用户名
             string email2 = "peter@163.com";//邮箱
             string password2 = "123456";//密码
             string roleName2 = "Users";
            var department1 = new Department { Name = "Marketing" };//机构
            var department2 = new Department { Name = "Accounting" };

            //如果没有Admin用户组则创建该组
            var role1 = roleManager.FindByName(roleName1);
            if (role1 == null)
            {
                role1 = new ApplicationRole() { Name = roleName1, Description = roleName1 };
                var roleresult = roleManager.Create(role1);
            }

            var role2 = roleManager.FindByName(roleName2);
            if (role2 == null)
            {
                role2 = new ApplicationRole() { Name = roleName2, Description = roleName2 };
                var roleresult = roleManager.Create(role2);
            }
            //如果没有admin@163.com用户则创建该用户
            var user1 = userManager.FindByName(name1);
            if (user1 == null)
            {
                user1 = new ApplicationUser
                {
                    UserName = name1,
                    Email = email1,
                };
                var result = userManager.Create(user1, password1);
               // result = userManager.SetLockoutEnabled(user1.Id, false);
            }

            var user2 = userManager.FindByName(name2);
            if (user2 == null)
            {
                user2 = new ApplicationUser
                {
                    UserName = name2,
                    Email = email2,
                };
                var result = userManager.Create(user2, password2);
                //result = userManager.SetLockoutEnabled(user2.Id, false);
            }

            // 把用户admin@163.com添加到用户组Admin中
            var rolesForUser1 = userManager.GetRoles(user1.Id);
            if (!rolesForUser1.Contains(role1.Name))
            {
                var result = userManager.AddToRole(user1.Id, role1.Name);
            }

            //var rolesForUser2 = userManager.GetRoles(user2.Id);
            //if (!rolesForUser2.Contains(role2.Name))
            //{
              //  var result = 
                    userManager.AddToRole(user2.Id, role2.Name);
            //}
            //添加机构
            var depart1 = db.Departments.FirstOrDefault(t => t.Name == department1.Name);
            if (depart1 == null)
            {
                db.Departments.Add(department1);
            }
            var depart2 = db.Departments.FirstOrDefault(t => t.Name == department2.Name);
            if (depart2 == null)
            {
                db.Departments.Add(department2);
            }
            //保存
            db.SaveChanges();

            //用户添加到机构
            db.Set<UserDepartment>().Add(new UserDepartment { DepartmentId = department1.Id, ApplicationUserId = user1.Id });
            db.Set<UserDepartment>().Add(new UserDepartment { DepartmentId = department2.Id, ApplicationUserId = user2.Id });
            db.SaveChanges();

        }
    }
}