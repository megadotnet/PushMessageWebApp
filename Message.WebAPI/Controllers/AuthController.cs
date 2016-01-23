using Message.WebAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Message.WebAPI.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    /// <see cref="http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/"/>
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// The _repo
        /// </summary>
        private AuthRepository _repo = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            _repo = new AuthRepository();
        }

        // POST api/Account/Register
        /// <summary>
        /// Registers the specified user model.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        /// <example><code>
        /// <![CDATA[
        /// POST http://localhost:2493/api/account/register HTTP/1.1
         ///
         ///Content-Type: application/json; charset=utf-8
         ///{
         ///  "userName": "peter",
         ///  "password": "SuperPass",
         ///  "confirmPassword": "SuperPass"
         ///}
        /// ]]>
        /// </code></example>
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets the error result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
