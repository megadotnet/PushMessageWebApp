using System;
using Message.WebAPI.Services.IRepository;
using Message.WebAPI.Services.Repository;
using BusniessEntities.Models;
using Xunit;

namespace Message.UnitTest
{
    public class MessageRepositoryTests
    {
        [Fact]
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
                Users = new string[] { "peter@163.com","john@gmail.com"}
            });

            Assert.True(isSuccess);

        }
    }
}
