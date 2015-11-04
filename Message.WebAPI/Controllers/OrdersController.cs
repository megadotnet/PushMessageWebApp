using Message.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Message.WebAPI.Controllers
{
    /// <summary>
    /// OrdersController
    /// </summary>
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// <![CDATA[
        /// POST http://localhost:2493/token HTTP/1.1
        ///Content-Type: x-www-form-urlencoded
        ///grant_type=password&username=peter&password=SuperPass
        /// 
        /// 
        /// GET http://localhost:2493/api/Orders HTTP/1.1
        ///Content-Type: application/json; charset=utf-8
        ///Authorization: Bearer 0E-S7DWjVH6yWlkgO0ErvCfvXlhZhb9edDYpiBENXBbCkI097JP8_O22EQEco6G8HOHHzGhLggI6zQMLq470kXvLNQpq5yn4yfjYF-5DvA2wKF1h_yekX5eEgFcwpLfoYwOiVocuFJHxkdp0BvSvtJut8skaQ7SnIXgUljx_QVUgTlFXuR20L6CxGcWO7wo2g9M1e55MIGUJOBuPI-eREtkFna8YQgE5J840ROwzenM
        /// 
        /// 
        /// ]]>
        /// </example>
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(Order.CreateOrders());
        }

    }

}