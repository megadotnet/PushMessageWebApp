using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Message.WebAPI.Models
{

    /// <summary>
    /// AuthRepository
    /// </summary>
    public class AuthRepository : IDisposable
    {
        /// <summary>
        /// The _CTX
        /// </summary>
        private AuthContext _ctx;

        /// <summary>
        /// The _user manager
        /// </summary>
        private UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthRepository"/> class.
        /// </summary>
        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }


}