using System.Collections.Generic;
using CourierKata.Primary.Ports.DataContracts;

namespace CourierKata.Primary.Ports.OperationContracts
{
    public interface IOrdersAdapter: IBaseAdapter
    {
        IEnumerable<OrderItem> GetOrdersReport(OrderCart cart);
    }
}