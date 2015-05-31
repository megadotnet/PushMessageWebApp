using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Message.WebAPI.Services.IRepository;
using Message.WebAPI.Services.Repository;

namespace Message.UnitTest
{
    [TestClass]
    public class MessageRepositoryTests
    {
        [TestMethod]
        public void ShouldSendMessage()
        {
            IMessageRepository messageRepository = new MessageRepository();
            bool isSuccess = messageRepository.SendMessage(new BusniessEntities.Models.PushMsg()
            {
                Id = 1,
                MSGTITLE = "Test title",
                MSGCONTENT = "test content",
                MSGTYPE=1,
                MsgSendType=1,
                ExpirationTime=DateTime.MaxValue,
                IsRead=false,
                Users = new string[] { "peter","john"}
            });

            Assert.IsTrue(isSuccess);

        }
    }
}
