using CourierKata.Primary.Ports.DataContracts;

namespace CourierKata.Primary.Ports.OperationContracts
{
    public interface IOrdersService
    {
        OrdersReport GetOrdersReport(OrderCart cart);
    }
}