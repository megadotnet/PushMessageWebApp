using Xunit;
using Message.WebAPI.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Message.WebAPI.Controllers.Api.Tests
{
    /// <summary>
    /// MessageControllerTests
    /// </summary>
    public class MessageControllerTests
    {
        /// <summary>
        /// Sends the message with authorization token test.
        /// </summary>
        /// <returns></returns>
        [Fact()]
        public async Task SendMessageWithAuthorizationTokenTest()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:2493/");

                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("Authorization-Token", "22,96,55,0,23,188,37,68,229,169,68,36,95,170,97,165,133,57,25,229,92,132,245,72,100,110,90,244,126,82,142,181,232,86,121,64,43,250,57,165,79,133,231,34,208,65,132,99,82,152,73,195,0,0,230,134,76,232,72,157,38,155,174,216,98,222,127,195,182,184,199,165,73,204,105,17,206,43,109,231,73,105,21,74,99,249,211,164,130,177,127,222,159,65,28,62,40,176,198,179,247,71,21,223,185,17,231,171,167,199,35,90,81,148,205,31,250,229,215,241,214,136,84,173,127,35,48,53");

                string results=await client.GetStringAsync("api/Message");

                Assert.NotNull(results);
            }
        }


        /// <summary>
        /// Sends the message with out authorization token test.
        /// </summary>
        [Fact()]
        public void SendMessageWithOutAuthorizationTokenTest()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:2493/");

                    client.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));

                    string results = await client.GetStringAsync("api/Message");
                }
            });

        }
    }
}