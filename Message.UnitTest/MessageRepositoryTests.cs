using System;
using Message.WebAPI.Services.IRepository;
using Message.WebAPI.Services.Repository;
using BusniessEntities.Models;
using Xunit;
using WebAuth.Hubs;
using System.Dynamic;
using Moq;
using Microsoft.AspNet.SignalR.Hubs;

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

        /// <summary>
        /// Hubses the are mockable via dynamic.
        /// </summary>
        /// <see cref="http://www.asp.net/signalr/overview/testing-and-debugging/unit-testing-signalr-applications"/>
        [Fact]
        public void HubsAreMockableViaDynamic()
        {
            bool sendCalled = false;
            var hub = new ChatHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.messageReceived = new Action<string, string>((name, message) =>
            {
                sendCalled = true;
            });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            hub.SendMessageToAll("peter", "TestMessage");
            Assert.True(sendCalled);
        }
    }
}
