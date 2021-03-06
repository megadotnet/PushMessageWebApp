﻿using Microsoft.AspNet.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Megadotnet.MessageMQ.Adapter;
using WebAuth;
using Megadotnet.MessageMQ.Adapter.Config;
using IronFramework.Common.Logging.Logger;
using BusniessEntities.Models;

namespace WebAuth
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
            string queueDestname = MyMQConfig.QueueDestination;
            Task.Factory.StartNew(() =>
            {

#if DEBUG
                queueDestname = MyMQConfig.QueueDestination;
#endif
                var activemq = ActiveMQListenAdapter<PushMsg>.Instance(MyMQConfig.MQIpAddress, queueDestname);

                activemq.MQListener += m =>
                {
                    CallClientMethod(m);
                };

                activemq.ReceviceListener<PushMsg>();
            });

        }

        /// <summary>
        /// Calls the client method.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <exception cref="System.ArgumentNullException">User list should not be null</exception>
        private static void CallClientMethod(PushMsg m)
        {
            if (m.Users == null)
                throw new ArgumentNullException("User list should not be null");

            var userAccounts = m.Users.ToArray();

            log.InfoFormat("Get Message from MQ 从MQ收到消息{0}, Users: {1}", m.MSGCONTENT, String.Join(",",m.Users));
            //GlobalHost.ConnectionManager.GetHubContext<FeedHub>().Clients.All.receive(m);

            //Tips:WebAuth will receive message By specifc user account while login system, ex:peter@163.com, account need to login
            GlobalHost.ConnectionManager.GetHubContext<FeedHub>().Clients.Users(userAccounts).receive(m);

        }
    }
}