
using Messag.Utility.Config;

using Message.WebAPI.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Messag.Logger;

using Message.WebAPI.Controllers.Api;
using BusniessEntities.Models;
using Megadotnet.MessageMQ.Adapter;
using Message.WebAPI.Models;
using BusinessEntities;

namespace Message.WebAPI.Services.Repository
{
    /// <summary>
    /// MessageRepository
    /// </summary>
    public class MessageRepository:IMessageRepository
    {
        /// <summary>
        /// The log
        /// </summary>
        private static ILogger log = new Logger("MessageRepository");

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messagemodel"></param>
        /// <returns></returns>
        public bool SendMessage(PushMsg messagemodel)
        {

            return PushMessages(new PushMsg[]{messagemodel});
          //var activemq = new ActiveMQAdapter<PushMessageModel>(MQConfig.MQIpAddress, MQConfig.QueueDestination);
          //return activemq.SendMessage<PushMessageModel>(messagemodel)>0;
        }


        /// <summary>
        /// Pushes the messages.
        /// </summary>
        /// <param name="pushMsgs">The push MSGS.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Push Message model should not be null</exception>
        /// <exception cref="System.ArgumentException">Insert PushMessage Model to Db Fail</exception>
        public bool PushMessages(PushMsg[] pushMsgs)
        {
            if (pushMsgs == null && pushMsgs.Length > 0)
            {
                throw new ArgumentNullException("Push Message model should not be null");
            }

            bool isSendSuccess = false;
            foreach (var messagebody in pushMsgs)
            {
                //Write Db and get Pk list
                int[] results = InsertPushMessage(messagebody);
                if (results == null && results.Length == 0)
                {
                    throw new ArgumentException("Insert PushMessage Model to Db Fail");
                }

                //set Id of Db to Common MQ model 
                messagebody.Id = results.FirstOrDefault();
                //send to front
                isSendSuccess = PushMessageToMQ(messagebody);
            }
            return isSendSuccess;
        }

        /// <summary>
        /// Inserts the push message.
        /// </summary>
        /// <param name="pushmsg">The pushmsg.</param>
        /// <returns>成功插入PushMessage的PK的列表</returns>
        //[TransactionScopeCallHandler]
        public int[] InsertPushMessage(PushMsg pushmsg)
        {
            var dbcontext = new MessageCenterEntities();

            IList<T_BD_PushMessage> pushMessageDtoList = new List<T_BD_PushMessage>();
            pushmsg.Users.ToList().ForEach(u =>
            {
                pushMessageDtoList.Add(
                    new T_BD_PushMessage()
                    {
                        IsRead = pushmsg.IsRead,
                        MsgTitle = pushmsg.MSGTITLE,
                        MsgContent = pushmsg.MSGCONTENT,
                        MsgType = Convert.ToInt32(pushmsg.MSGTYPE),
                        MsgSendType = pushmsg.MsgSendType,
                        ExpirationTime = pushmsg.ExpirationTime,
                         T_BD_PushMessageToUsers=new List<T_BD_PushMessageToUsers> {
                             new T_BD_PushMessageToUsers() { Userid = GetUserIdListByUserAccount(u), SendingTime=DateTime.Now }}
                       // Userid = GetUserIdListByUserAccount(u)
                    }
                    );
            });

            IList<int> pushMessageEntiyIdList = new List<int>();
            pushMessageDtoList.ToList().ForEach(u =>
            {
                //save db

                try
                {
                    dbcontext.T_BD_PushMessage.Add(u);
                    dbcontext.SaveChanges();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }

                pushMessageEntiyIdList.Add(u.Id);
            });


            return pushMessageEntiyIdList.ToArray();
 
        }


        /// <summary>
        /// Pushes the message to mq.
        /// </summary>
        /// <param name="pushmsg">The pushmsg.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// PushMsg object should not be null
        /// or
        /// User list should not be null
        /// </exception>
        protected bool PushMessageToMQ(PushMsg pushmsg)
        {
            if (pushmsg == null)
            {
                throw new ArgumentNullException("PushMsg object should not be null");
            }

            if (pushmsg.Users == null)
            {
                throw new ArgumentNullException("User list should not be null");
            }

            //判断过期logic
            if (pushmsg.ExpirationTime >= DateTime.Now)
            {
                // Clients.
                return SendPushMessageToMQ(pushmsg);
            }
            return false;
        }


        /// <summary>
        /// Gets the user identifier list by user account.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        /// <returns></returns>
        //private int GetUserIdListByUserAccount(string userAccount)
        //{
        //    return 0;
        //}

        private string GetUserIdListByUserAccount(string userAccount)
        {
            return userAccount;
        }

        /// <summary>
        /// Sends the push message.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private bool SendPushMessageToMQ(PushMsg msg)
        {
            var activemq = CreateActiveMQInstance();
            int sendflag = activemq.SendMessage<PushMsg>(msg);

            return sendflag > 0 ? true : false;
        }


        /// <summary>
        /// Creates the active mq instance.
        /// </summary>
        /// <returns></returns>
        private static ActiveMQAdapter<PushMsg> CreateActiveMQInstance()
        {
            var activemq = new ActiveMQAdapter<PushMsg>(MQConfig.MQIpAddress, MQConfig.QueueDestination);
            return activemq;
        }
    }
}