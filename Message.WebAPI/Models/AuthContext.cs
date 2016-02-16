using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Message.WebAPI.Models
{
    /// <summary>
    /// AuthContext
    /// </summary>
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthContext"/> class.
        /// </summary>
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}