
using Messag.Utility.Config;

using Message.WebAPI.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Message.Utility.Helper;
using Messag.Logger;

using Message.WebAPI.Controllers.Api;
using BusniessEntities.Models;
using Megadotnet.MessageMQ.Adapter;

namespace Message.WebAPI.Services.Repository
{
    public class MessageRepository:IMessageRepository
    {
        private static ILogger log = new Logger("MessageRepository");

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messagemodel"></param>
        /// <returns></returns>
        public bool SendMessage(PushMessageModel messagemodel)
        {
          var activemq = new ActiveMQAdapter<PushMessageModel>(MQConfig.MQIpAddress, MQConfig.QueueDestination);
          return activemq.SendMessage<PushMessageModel>(messagemodel)>0;
        }
    }
}