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
    /// IPHostValidationAttribute
    /// </summary>
    public class IPHostValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            var context = actionContext.Request.Properties["MS_HttpContext"] as System.Web.HttpContextBase;
            string userIP = context.Request.UserHostAddress;
            try
            {
                AuthorizedIPRepository.GetAuthorizedIPs().First(x => x == userIP);
            }
            catch (Exception)
            {
                actionContext.Response =
                   new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                   {
                       Content = new StringContent("Unauthorized IP Address")
                   };
                return;
            }
        }
    }

    public class AuthorizedIPRepository
    {
        public static IQueryable<string> GetAuthorizedIPs()
        {

            var ips = new List<string>();

            ips.Add("127.0.0.1");
            ips.Add("::1");

            return ips.AsQueryable();
        }
    }
}