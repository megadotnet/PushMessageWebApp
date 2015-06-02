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
    public class MessageController : ApiController
    {
        private static ILogger log = new Logger("MessageController");

        /// <summary>
        /// The address.
        /// </summary>
        private static string address = MQConfig.MQIpAddress;
        /// <summary>
        /// The queu e_ destination.
        /// </summary>
        private static string QUEUE_DESTINATION = MQConfig.QueueDestination;

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
        /// Get
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            return Ok("Call method Ok");
        }

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <param name="messagemodel">The messagemodel.</param>
        /// <returns></returns>
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
                    log.ErrorFormat("发送失败！Sent fail ! {0}", messagemodel);
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
