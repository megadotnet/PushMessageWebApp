using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAuth.Models
{
    public class MySqlConfiguration : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlConfiguration"/> class.
        /// </summary>
        public MySqlConfiguration()
        {
            SetHistoryContext(
            "MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }
    }
}