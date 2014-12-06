using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Message.WebAPI.Controllers.Api
{
    public class TestController : ApiController
    {
        public string GetTest()
        {
            return "testGet";
        }

        public string PostTest()
        {
            return "testPost";
        }
    }
}
