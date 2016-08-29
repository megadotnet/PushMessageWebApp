using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebAuth.App_Start
{
    /// <summary>
    /// MyValidateAntiForgeryToken
    /// </summary>
    public class MyValidateAntiForgeryToken : FilterAttribute, IAuthorizationFilter
    {

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <exception cref="System.ArgumentNullException">filterContext</exception>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            string cookieToken = string.Empty;
            string formToken = string.Empty;

            string tokenHeaders = string.Empty;

            tokenHeaders = filterContext.RequestContext.HttpContext.Request.Headers.Get("RequestVerificationToken");

            if (!string.IsNullOrEmpty(tokenHeaders))
            {
                string[] tokens = tokenHeaders.Split(':');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }
            AntiForgery.Validate(cookieToken, formToken);
        }
        
    }
}