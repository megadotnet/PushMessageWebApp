using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Message.WebAPI.Services
{
    /// <summary>
    /// TokenValidationAttribute
    /// </summary>
    /// <example><code>
    /// <![CDATA[
    /// GET http://localhost:2493/api/message HTTP/1.1
    ///Authorization-Token:22,96,55,0,23,188,37,68,229,169,68,36,95,170,97,165,133,57,25,229,92,132,245,72,100,110,90,244,126,82,142,181,232,86,121,64,43,250,57,165,79,133,231,34,208,65,132,99,82,152,73,195,0,0,230,134,76,232,72,157,38,155,174,216,98,222,127,195,182,184,199,165,73,204,105,17,206,43,109,231,73,105,21,74,99,249,211,164,130,177,127,222,159,65,28,62,40,176,198,179,247,71,21,223,185,17,231,171,167,199,35,90,81,148,205,31,250,229,215,241,214,136,84,173,127,35,48,53
    /// ]]>
    /// </code></example>
    public class TokenValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string token;

            try
            {
                token = actionContext.Request.Headers.GetValues("Authorization-Token").First();
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Missing Authorization-Token")
                };
                return;
            }

            try
            {
                AuthorizedUserRepository.GetUsers().First(x => x.Name == RSAClass.Decrypt(token));
                base.OnActionExecuting(actionContext);
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Unauthorized User")
                };
                return;
            }
        }
    }
}