﻿using CourierKata.Primary.Ports.DataContracts;
using CourierKata.Primary.Ports.OperationContracts;
using Microsoft.AspNetCore.Mvc;

namespace CourierKata.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersAdapter _ordersAdapter;

        public OrdersController(IOrdersAdapter ordersAdapter)
            => _ordersAdapter = ordersAdapter;

        [HttpPost]
        public OrdersReport Post([FromBody] OrderCart cart)
            => _ordersAdapter.GetOrdersReport(cart);
    }
}