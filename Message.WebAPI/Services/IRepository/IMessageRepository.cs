
using BusniessEntities.Models;
using Message.WebAPI.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Message.WebAPI.Services.IRepository
{
    /// <summary>
    /// IMessageRepository
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="messagemodel">The messagemodel.</param>
        /// <returns></returns>
        bool SendMessage(PushMsg messagemodel);
    }
}