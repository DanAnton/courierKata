using System.Collections.Generic;
using System.Linq;
using CourierKata.Primary.Ports.DataContracts;
using CourierKata.Primary.Ports.OperationContracts;

namespace CourierKata.Domain.Implementation
{
    public class OrdersService : IOrdersService
    {
        public OrdersReport GetOrdersReport(OrderCart cart)
        {
            var result = new Dictionary<ProductType, double>();
            cart.Products.ToList().ForEach(p => AddValuesToDictionary(result, p));

            return new OrdersReport
                   {
                       Items = result.Select(p => new OrderItem
                                                  {
                                                      Type = p.Key.ToString(),
                                                      Cost = p.Value
                                                  }).ToList(),
                       Total = result.Values.Sum()
                   };
        }

        private static void AddValuesToDictionary(Dictionary<ProductType, double> result, Product p)
        {
            var type = ProductType.SmallParcel;
            var price = 3.0;
            if (p.Dimension < 10) {
                price = p.Quantity * 3;
            }
            else if (p.Dimension < 50) {
                type = ProductType.MediumParcel;
                price = p.Quantity * 8;
            }
            else if (p.Dimension < 100) {
                type = ProductType.LargeParcel;
                price = p.Quantity * 15;
            }
            else if (p.Dimension >= 100) {
                type = ProductType.XlParcel;
                price = p.Quantity * 25;
            }

            if (result.Keys.Contains(type))
                result[type] += price;
            else
                result.Add(type, price);
        }
    }
}