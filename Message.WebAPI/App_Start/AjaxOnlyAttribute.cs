using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Message.WebAPI.App_Start
{
    /// <summary>
    /// AjaxOnlyAttribute
    /// </summary>
    public class AjaxOnlyAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var headers = request.Headers;
            if (!headers.Contains("X-Requested-With") || headers.GetValues("X-Requested-With").FirstOrDefault() != "XMLHttpRequest")
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
   
        }
    }
}