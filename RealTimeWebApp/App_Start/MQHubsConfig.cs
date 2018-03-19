
using BusniessEntities.Models;
using IronFramework.Common.Logging.Logger;
using Megadotnet.MessageMQ.Adapter.Config;
using Microsoft.AspNet.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




namespace RealTimeApp
{
    /// <summary>
    /// MQHubsConfig
    /// </summary>
    public class MQHubsConfig
    {
        /// <summary>
        /// ILogger
        /// </summary>
        private static ILogger log = new Logger("MQHubsConfig");

        /// <summary>
        /// Registers the mq listen and hubs.
        /// </summary>
        public static void RegisterMQListenAndHubs()
        {
            var activemq = Megadotnet.MessageMQ.Adapter.ActiveMQListenAdapter<PushMsg>
                .Instance(MyMQConfig.MQIpAddress, MyMQConfig.QueueDestination);
            activemq.MQListener += m =>
            {
                log.InfoFormat("从MQ收到消息{0}", m.MSGCONTENT);

                //Tips:RealTimeApp Will receive message By Using Clients All
                GlobalHost.ConnectionManager.GetHubContext<FeedHub>().Clients.All.receive(m);

                //Tips:WebAuth will receive message By specifc user account while login system
                //GlobalHost.ConnectionManager.GetHubContext<FeedHub>().Clients.Users(m.Users).receive(m);
            };

            activemq.ReceviceListener<PushMsg>();
        }
    }
}