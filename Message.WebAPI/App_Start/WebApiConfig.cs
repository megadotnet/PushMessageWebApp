using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Microsoft.Practices.Unity;
using Message.WebAPI.Services.IRepository;
using Message.WebAPI.Services.Repository;
using Messag.Utility.EntLib.IoC;
using System.Net.Http.Headers;
using System.Web.Http.ExceptionHandling;
using Elmah.Contrib.WebApi;
using Message.WebAPI.Areas.HelpPage;
using System.Web;

namespace Message.WebAPI
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //http://blog.elmah.io/logging-to-elmah-io-from-web-api/
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

            //http://www.asp.net/web-api/overview/testing-and-debugging/tracing-in-aspnet-web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
