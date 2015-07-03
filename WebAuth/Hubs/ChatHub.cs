using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAuth.Hubs
{
    public class ChatHub : Hub
    {
        #region Data Members

        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        #endregion

        #region Methods

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="userName"></param>
        public void Connect(string userName)
        {
            //TODO:consider use database id instead context.ConnectionId
            var id = Context.ConnectionId;


            if (ConnectedUsers.Count(x => x.UserName == userName) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName });

                // send to caller
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

            }
            else
            {
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

            }
         

        }

        /// <summary>
        /// SendMessageToAll user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            AddMessageinCache(userName, message);

            // Broad cast message
            Clients.All.messageReceived(userName, message);
        }

        /// <summary>
        /// SendPrivateMessage 
        /// </summary>
        /// <param name="toUserId">user</param>
        /// <param name="message"></param>
        public void SendPrivateMessage(string toUserId, string message)
        {
            string tempusername = Context.User.Identity.Name;
            string fromUserId = tempusername.Substring(0, tempusername.IndexOf('@'));

            var toUser = ConnectedUsers.FirstOrDefault(x => x.UserName == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.UserName == fromUserId);

            if (toUser != null && fromUser != null)
            {
                // send to target User, when use asp.net form auth
                Clients.User(toUserId+"@163.com").sendPrivateMessage(fromUserId, fromUser.UserName, message);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }

        }

        /// <summary>
        /// OnDisconnected
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string tempusername = Context.User.Identity.Name;
            tempusername = tempusername.Substring(0, tempusername.IndexOf('@'));
            var item = ConnectedUsers.FirstOrDefault(x => x.UserName == tempusername);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }
            return base.OnDisconnected(stopCalled);
        }


        #endregion

        #region private Messages

        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        #endregion
    }

    public class MessageDetail
    {

        public string UserName { get; set; }

        public string Message { get; set; }

    }

    public class UserDetail
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
    }
}