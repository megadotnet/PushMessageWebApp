using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebAuth
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Configure();
            MQHubsConfig.RegisterMQListenAndHubs();

            //http://www.cnblogs.com/xuanhun/p/3611768.html
            MvcHandler.DisableMvcResponseHeader = true;
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception lastException = Server.GetLastError();
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Fatal("Request: '{0}'\n Exception:{1}", HttpContext.Current.Request.Url, lastException);
        }
    }
}
