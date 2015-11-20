using BusinessEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WebAuth.Models
{
    /// <summary>
    /// ApplicationDbContext
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <see cref="https://msdn.microsoft.com/en-us/data/jj819164.aspx"/>
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        //static ApplicationDbContext()
        //{
            //Database.SetInitializer<ApplicationDbContext>(new MySqlInitializer());
        //}


        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <exception cref="System.ArgumentNullException">modelBuilder</exception>
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
            //ChineseName of ApplicationUser
            configuration4.Property(x => x.ChineseName).HasMaxLength(30);

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public new IDbSet<ApplicationRole> Roles { get; set; }
        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public virtual IDbSet<ApplicationPermission> Permissions { get; set; }
        /// <summary>
        /// Gets or sets the departments.
        /// </summary>
        /// <value>
        /// The departments.
        /// </value>
        public virtual DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Gets or sets the t_ b d_ push message. 推送消息表
        /// </summary>
        /// <value>
        /// The t_ b d_ push message.
        /// </value>
        public virtual DbSet<T_BD_PushMessage> T_BD_PushMessage { get; set; }

        /// <summary>
        /// Gets or sets the t_ b d_ push message configuration. 推送消息配置表
        /// </summary>
        /// <value>
        /// The t_ b d_ push message configuration.
        /// </value>
        public virtual DbSet<T_BD_PushMessageConfig> T_BD_PushMessageConfig { get; set; }
    }
}