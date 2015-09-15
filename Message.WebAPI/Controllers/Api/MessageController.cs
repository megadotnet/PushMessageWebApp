using BusniessEntities.Models;
using Messag.Logger;
using Messag.Utility.Config;
using Message.WebAPI.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Message.WebAPI.Controllers.Api
{
    /// <summary>
    /// MessageController
    /// </summary>
    public class MessageController : ApiController
    {
        /// <summary>
        /// The log
        /// </summary>
        private static ILogger log = new Logger("MessageController");

        /// <summary>
        /// The address.
        /// </summary>
        private static string address = MQConfig.MQIpAddress;
        /// <summary>
        /// The queu e_ destination.
        /// </summary>
        private static string QUEUE_DESTINATION = MQConfig.QueueDestination;

        /// <summary>
        /// The message repository
        /// </summary>
        private IMessageRepository messageRepository;

        /// <summary>
        /// MessageController
        /// </summary>
        /// <param name="_messageRepository"></param>
        public MessageController(IMessageRepository _messageRepository)
        {
            this.messageRepository = _messageRepository;
        }

        /// <summary>
        /// Get string 
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        public IHttpActionResult Get()
        {
            return Ok("Call method Ok");
        }

        /// <summary>
        /// Send message to client
        /// </summary>
        /// <param name="messagemodel">The messagemodel.</param>
        /// <returns>IHttpActionResult</returns>
        [HttpPost]
        public IHttpActionResult SendMessage(PushMsg messagemodel)
        {
            return SendToServer(messagemodel);
        }

        /// <summary>
        /// Sends to server.
        /// </summary>
        /// <param name="messagemodel">The messagemodel.</param>
        /// <returns></returns>
        private IHttpActionResult SendToServer(PushMsg messagemodel)
        {

            if (ModelState.IsValid)
            {
                if (messageRepository.SendMessage(messagemodel))
                {
                    log.Debug("发送成功 Send success！");
                    return Ok();
                }
                else
                {
                    log.ErrorFormat("发送失败！Sent failure ! {0}", messagemodel);
                    return Content(HttpStatusCode.ExpectationFailed, new Exception("send message error"));
                }
            }
            else
            {
                log.ErrorFormat("参数验证失败！ModelState is not Valid{0}", messagemodel);
                return Content(HttpStatusCode.BadRequest, ModelState);
            }

        }

  
    }
}
