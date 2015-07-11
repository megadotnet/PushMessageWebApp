using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebAuth.Hubs;
using Xunit;

namespace Message.UnitTest
{
    public class ChatHubTests
    {
        /// <summary>
        /// Hubses the are mockable via dynamic.
        /// </summary>
        /// <see cref="http://www.asp.net/signalr/overview/testing-and-debugging/unit-testing-signalr-applications"/>
        [Fact]
        public void HubsAreMockableViaDynamicSendMessageToAllTests()
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

        /// <summary>
        /// Hubses the are mockable via dynamic send private message tests.
        /// </summary>
        [Fact]
        public void HubsAreMockableViaDynamicSendPrivateMessageTests()
        {
            bool sendCalled = false;
            string connectionId = "1";
            var hub = new ChatHub();
         
            var request = new Mock<IRequest>();
            request.Setup(s => s.User.Identity.Name).Returns("peter@163.com");
            request.Setup(s => s.User.Identity.IsAuthenticated).Returns(true);

            var mockContext = new Mock<HubCallerContext>(request.Object, connectionId);

            var mockIdentiy=new Mock<IIdentity>();
            mockIdentiy.Setup(ii=>ii.Name).Returns("peter@163.com");
            mockIdentiy.Setup(ii => ii.IsAuthenticated).Returns(true);
           
             
            var mockUser = new Mock<IPrincipal>();
            mockUser.Setup(dd => dd.Identity).Returns(mockIdentiy.Object);

            mockContext.Setup(cc => cc.User).Returns(mockUser.Object);
            mockContext.Setup(cc => cc.ConnectionId).Returns(connectionId);
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.sendPrivateMessage = new Action<string, string>((name, message) =>
            {
                sendCalled = true;
            });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            hub.Context = mockContext.Object;

            //act
            hub.OnConnected();
            hub.SendPrivateMessage("peter", "TestMessage");
            Assert.True(sendCalled);
        }
    }
}
