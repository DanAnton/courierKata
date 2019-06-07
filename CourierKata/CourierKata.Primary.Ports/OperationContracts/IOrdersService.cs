﻿using System.Collections.Generic;
using CourierKata.Primary.Ports.DataContracts;

namespace CourierKata.Primary.Ports.OperationContracts
{
    public interface IOrdersService
    {
        IEnumerable<OrderItem> GetOrdersReport(OrderCart cart);
    }
}