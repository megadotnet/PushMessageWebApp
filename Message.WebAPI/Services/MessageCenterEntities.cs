using BusinessEntities;
using Message.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Message.WebAPI.Services
{
    /// <summary>
    /// MessageCenterEntities
    /// </summary>
    public class MessageCenterEntities : DbContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageCenterEntities"/> class.
        /// </summary>
        public MessageCenterEntities()
            : base("MessageCenterEntities")
        {
        }

        /// <summary>
        /// Gets or sets the t_ b d_ push message.
        /// </summary>
        /// <value>
        /// The t_ b d_ push message.
        /// </value>
        public DbSet<T_BD_PushMessage> T_BD_PushMessage { get; set; }
        /// <summary>
        /// Gets or sets the t_ b d_ push message configuration.
        /// </summary>
        /// <value>
        /// The t_ b d_ push message configuration.
        /// </value>
        public DbSet<T_BD_PushMessageConfig> T_BD_PushMessageConfig { get; set; }

        /// <summary>
        /// 推送消息表Users
        /// </summary>
        public virtual DbSet<T_BD_PushMessageToUsers> T_BD_PushMessageToUsers { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}