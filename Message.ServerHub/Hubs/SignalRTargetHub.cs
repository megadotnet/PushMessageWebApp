// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalRTargetHub.cs" company="Megadotnet">
//   SignalRTargetHub
// </copyright>
// <summary>
//   SignalRTargetHub
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebAuth.Hubs
{
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// SignalRTargetHub
    /// </summary>
    /// <see cref="http://www.codeproject.com/Articles/758633/Streaming-logs-with-SignalR"/>
    /// <seealso cref="http://www.cnblogs.com/Irving/archive/2013/01/13/2858601.html"/>
    public class SignalRTargetHub : Hub
    {
        /// <summary>
        /// The signal r hub
        /// </summary>
        private static IHubContext signalRHub;

        /// <summary>
        /// Helloes this instance.
        /// </summary>
        public void Hello()
        {
            this.Clients.Caller.logEvent(
                DateTime.UtcNow.ToLongTimeString(),
                "info",
                "SignalR connected");
        }


        /// <summary>
        /// Sends the specified longdate.
        /// </summary>
        /// <param name="longdate">The longdate.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        public static void Send(string longdate, string logLevel, String message)
        {
            if (signalRHub == null)
            {
                signalRHub = GlobalHost.ConnectionManager.GetHubContext<SignalRTargetHub>();
            }
            if (signalRHub != null)
            {
                signalRHub.Clients.All.logEvent(longdate, logLevel, message);
            }
        }
    }
}