using System.Collections.Generic;
using CourierKata.Primary.Ports.DataContracts;
using CourierKata.Primary.Ports.OperationContracts;

namespace CourierKata.Domain.Implementation
{
    public class OrdersService : IOrdersService {
        public IEnumerable<OrderItem> GetOrdersReport(OrderCart cart) 
            => throw new System.NotImplementedException();
    }
}