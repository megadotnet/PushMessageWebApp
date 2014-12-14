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
            bool isSuccess=messageRepository.SendMessage(new BusniessEntities.Models.PushMessageModel()
            {
                  Id=1,
                  MSG_TITLE="Test title",
                   MSG_CONTENT="test content"
            });

            Assert.IsTrue(isSuccess);

        }
    }
}
