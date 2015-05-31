using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Message.WebAPI.Services.IRepository;
using Message.WebAPI.Services.Repository;
using BusniessEntities.Models;

namespace Message.UnitTest
{
    [TestClass]
    public class MessageRepositoryTests
    {
        [TestMethod]
        public void ShouldSendMessage()
        {
            IMessageRepository messageRepository = new MessageRepository();
            bool isSuccess = messageRepository.SendMessage(new PushMsg()
            {
                Id = 1,
                MSGTITLE = "Test title"+DateTime.Now,
                MSGCONTENT = "test content" + DateTime.Now,
                MSGTYPE="1",
                MsgSendType="1",
                ExpirationTime=DateTime.MaxValue,
                IsRead=false,
                Users = new string[] { "peter","john"}
            });

            Assert.IsTrue(isSuccess);

        }
    }
}
