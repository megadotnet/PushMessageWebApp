using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAuth.Hubs
{
    /// <summary>
    /// SignalRTargetHub
    /// </summary>
    /// <see cref="http://www.codeproject.com/Articles/758633/Streaming-logs-with-SignalR"/>
    /// <seealso cref="http://www.cnblogs.com/Irving/archive/2013/01/13/2858601.html"/>
    public class SignalRTargetHub : Hub
    {
        private static IHubContext signalRHub;

        public void Hello()
        {
            this.Clients.Caller.logEvent(
                DateTime.UtcNow.ToLongTimeString(),
                "info",
                "SignalR connected");
        }
  

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