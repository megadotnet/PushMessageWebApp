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

    #region Helpers

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }

        public static List<Order> CreateOrders()
        {
            List<Order> OrderList = new List<Order> 
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };

            return OrderList;
        }
    }

    #endregion
}