using CourierKata.Primary.Ports.DataContracts;

namespace CourierKata.Primary.Ports.OperationContracts
{
    public interface IOrdersAdapter : IBaseAdapter
    {
        OrdersReport GetOrdersReport(OrderCart cart);
    }
}