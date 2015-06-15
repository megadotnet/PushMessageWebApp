﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WebAuth.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        //static ApplicationDbContext()
        //{
            //Database.SetInitializer<ApplicationDbContext>(new MySqlInitializer());
        //}


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            base.OnModelCreating(modelBuilder);

            //配置permission与rolePermission的1对多关系
            EntityTypeConfiguration<ApplicationPermission> configuration = modelBuilder.Entity<ApplicationPermission>().ToTable("ApplicationPermissions");
            configuration.HasMany<ApplicationRolePermission>(u => u.Roles).WithRequired().HasForeignKey(k => k.PermissionId);
            //配置role与persmission的映射表RolePermission的键
            modelBuilder.Entity<ApplicationRolePermission>().HasKey(r => new { PermissionId = r.PermissionId, RoleId = r.RoleId }).ToTable("RolePermissions");
            //配置role与RolePermission的1对多关系
            EntityTypeConfiguration<ApplicationRole> configuration2 = modelBuilder.Entity<ApplicationRole>();
            configuration2.HasMany<ApplicationRolePermission>(r => r.Permissions).WithRequired().HasForeignKey(k => k.RoleId);

            //配置Department与applicationUser的多对多关系
            modelBuilder.Entity<UserDepartment>().HasKey(u => new { u.ApplicationUserId, u.DepartmentId }).ToTable("UserDepartment").Property(t => t.ApplicationUserId).HasColumnName("UserId");

            EntityTypeConfiguration<Department> configuration3 = modelBuilder.Entity<Department>();
            configuration3.HasMany(d => d.Users).WithRequired().HasForeignKey(k => k.DepartmentId);

            EntityTypeConfiguration<ApplicationUser> configuration4 = modelBuilder.Entity<ApplicationUser>();
            configuration4.HasMany(d => d.Departments).WithOptional().HasForeignKey(k => k.ApplicationUserId);

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public new IDbSet<ApplicationRole> Roles { get; set; }
        public virtual IDbSet<ApplicationPermission> Permissions { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        //public virtual DbSet<T_BD_PushMessage> T_BD_PushMessage { get; set; }
        //public virtual DbSet<T_BD_PushMessageConfig> T_BD_PushMessageConfig { get; set; }
    }
}