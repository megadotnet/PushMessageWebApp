using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RealTimeApp.App_Start
{
    /// <summary>
    /// AuditLogActionFilter
    /// </summary>
    public class AuditLogActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        public string ActionType { get; set; }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Retrieve the information we need to log
            string routeInfo = GetRouteData(filterContext.RouteData);
            string user = filterContext.HttpContext.User.Identity.Name;

            // Write the information to "Audit Log"
            Debug.WriteLine(String.Format("ActionExecuting - {0} ActionType: {1}; User:{2}"
              , routeInfo, ActionType, user), "Audit Log");

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Retrieve the information we need to log
            string routeInfo = GetRouteData(filterContext.RouteData);
            bool isActionSucceeded = filterContext.Exception == null;
            string user = filterContext.HttpContext.User.Identity.Name;

            // Write the information to "Audit Log"
            Debug.WriteLine(String.Format("ActionExecuted - {0} ActionType: {1}; Executed successfully:{2}; User:{3}"
              , routeInfo, ActionType, isActionSucceeded, user), "Audit Log");

            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// Gets the route data.
        /// </summary>
        /// <param name="routeData">The route data.</param>
        /// <returns></returns>
        private string GetRouteData(RouteData routeData)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];

            return String.Format("Controller:{0}; Action:{1};", controller, action);
        }
    }
}