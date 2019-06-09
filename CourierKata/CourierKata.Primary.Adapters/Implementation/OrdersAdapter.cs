using CourierKata.Primary.Ports.DataContracts;
using CourierKata.Primary.Ports.OperationContracts;

namespace CourierKata.Primary.Adapters.Implementation
{
    public class OrdersAdapter : IOrdersAdapter
    {
        private readonly IOrdersService _service;

        public OrdersAdapter(IOrdersService service)
            => _service = service;

        public OrdersReport GetOrdersReport(OrderCart cart)
            => _service.GetOrdersReport(cart);
    }
}