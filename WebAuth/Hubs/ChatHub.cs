// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatHub.cs" company="Megadotnet">
//   ChatHub
// </copyright>
// <summary>
//   ChatHub
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebAuth.Hubs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;

    /// <summary>
    ///     ChatHub
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// The identity name suffix
        /// </summary>
        private static readonly string identityNameSuffix = "@163.com";

        #region Static Fields

        /// <summary>
        /// The connected users.
        /// </summary>
        private static readonly List<UserDetail> ConnectedUsers = new List<UserDetail>();

        /// <summary>
        /// The current message.
        /// </summary>
        private static readonly List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on connected.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task OnConnected()
        {
            // TODO:consider use database id instead context.ConnectionId
            string connectedId = this.Context.ConnectionId;
            IIdentity princpleUser = this.Context.User.Identity;

            string userId = princpleUser.GetUserId();
            //name like admin@163.com, we just want to remove @163.com
            string tempusername = princpleUser.Name;
            string userName = tempusername.Substring(0, tempusername.IndexOf('@'));

            if (ConnectedUsers.Count(x => x.UserName == userName) == 0)
            {
                ConnectedUsers.Add(new UserDetail { Id = userId, ConnectionId = connectedId, UserName = userName });

                // send to caller
                this.Clients.Caller.onConnected(connectedId, userName, ConnectedUsers, CurrentMessage);

                // send to all except caller client
                this.Clients.AllExcept(connectedId).onNewUserConnected(connectedId, userName);
            }
                
                // if user was existed in ConnectedUsers List, do not notify other client
            else
            {
                this.Clients.Caller.onConnected(connectedId, userName, ConnectedUsers, CurrentMessage);
            }

            return base.OnConnected();
        }

        /// <summary>
        /// OnDisconnected
        /// </summary>
        /// <param name="stopCalled">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            string tempusername = this.Context.User.Identity.Name;
            tempusername = tempusername.Substring(0, tempusername.IndexOf('@'));
            UserDetail item = ConnectedUsers.FirstOrDefault(x => x.UserName == tempusername);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                string id = this.Context.ConnectionId;
                this.Clients.All.onUserDisconnected(id, item.UserName);
            }

            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// SendMessageToAll user
        /// </summary>
        /// <param name="userName">
        /// </param>
        /// <param name="message">
        /// </param>
        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            this.AddMessageinCache(userName, message);

            // Broad cast message
            this.Clients.All.messageReceived(userName, message);
        }

        /// <summary>
        /// SendPrivateMessage
        /// </summary>
        /// <param name="toUserId">
        /// user
        /// </param>
        /// <param name="message">
        /// </param>
        public void SendPrivateMessage(string toUserId, string message)
        {
            string tempusername = this.Context.User.Identity.Name;
            string fromUserId = tempusername.Substring(0, tempusername.IndexOf('@'));

            UserDetail toUser = ConnectedUsers.FirstOrDefault(x => x.UserName == toUserId);
            UserDetail fromUser = ConnectedUsers.FirstOrDefault(x => x.UserName == fromUserId);

            if (toUser != null && fromUser != null)
            {
                // send to target User, when use asp.net form auth
                this.Clients.User(toUserId + identityNameSuffix).sendPrivateMessage(fromUserId, fromUser.UserName, message);

                // send to caller user
                this.Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add messagein cache.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            if (CurrentMessage.Count > 100)
            {
                CurrentMessage.RemoveAt(0);
            }
        }

        #endregion
    }

    /// <summary>
    /// The message detail.
    /// </summary>
    public class MessageDetail
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        #endregion
    }

    /// <summary>
    /// The user detail.
    /// </summary>
    public class UserDetail
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the connection id.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        #endregion
    }
}