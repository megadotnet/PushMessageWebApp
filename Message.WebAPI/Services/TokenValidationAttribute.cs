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
    ///Authorization-Token: 12,227,183,4,61,28,137,16,92,83,152,190,29,119,124,0,92,200,71,125,181,112,225,72,1,126,121,191,35,11,43,154,225,6,99,214,71,16,113,183,52,141,106,62,163,160,149,132,162,66,136,101,161,244,219,185,53,207,161,254,183,93,42,229,61,207,25,95,92,4,195,34,13,57,152,241,30,210,254,223,47,201,87,20,133,32,221,22,143,249,243,138,242,210,63,200,244,218,148,99,43,250,245,139,105,26,43,41,129,57,156,220,52,95,218,16,35,165,173,41,116,185,11,217,14,12,162,211
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