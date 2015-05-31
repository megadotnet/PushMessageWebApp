using Message.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Message.WebAPI.Services
{
    public class MessageCenterEntities : DbContext
    {

        public MessageCenterEntities()
            : base("MessageCenterEntities")
        {
        }

        public DbSet<T_BD_PushMessage> T_BD_PushMessage { get; set; }
        public DbSet<T_BD_PushMessageConfig> T_BD_PushMessageConfig { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}