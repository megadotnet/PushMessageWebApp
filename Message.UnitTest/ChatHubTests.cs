using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
