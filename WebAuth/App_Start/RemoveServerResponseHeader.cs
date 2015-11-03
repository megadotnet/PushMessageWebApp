using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAuth.App_Start
{
    /// <summary>
    /// RemoveServerResponseHeader
    /// </summary>
    /// <see cref="http://www.codeproject.com/Articles/1037948/Security-through-obscurity-Hiding-Asp-Net-MVC-resp"/>
    /// <example><code>
    /// <<system.webserver>
     ///    <modules>
     ///      <add name="CustomHeaderModule" type="VectorShop.Helpers.RemoveServerResponseHeader">
     ///    </add> 
     ///</modules>
     /// </system.webserver>
    /// </code></example>
    public class RemoveServerResponseHeader : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Called when [pre send request headers].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
        }
    }
}