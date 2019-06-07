using System;
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
            var ordersReport = ComputeOrdersReport(cart);
            AddSpeedyShipping(cart, ordersReport);
            RecalculateTotal(ordersReport);
            return ordersReport;
        }

        private static OrdersReport ComputeOrdersReport(OrderCart cart)
        {
            var result = new Dictionary<ProductType, double>();
            cart.Products.ToList().ForEach(p => BuildOrdersDictionary(result, p));

            var ordersReport = new OrdersReport
                               {
                                   Items = result.Select(p => new OrderItem
                                                              {
                                                                  Type = p.Key.ToString(),
                                                                  Cost = p.Value
                                                              }).ToList(),
                                   Total = result.Values.Sum()
                               };
            return ordersReport;
        }

        private static void AddSpeedyShipping(OrderCart cart, OrdersReport ordersReport)
        {
            if (!cart.HasSpeedyDeliver) return;
            ordersReport.Items.Add(new OrderItem
                                   {
                                       Type = "Speedy shipping",
                                       Cost = ordersReport.Total
                                   });
        }

        private static void RecalculateTotal(OrdersReport ordersReport)
            => ordersReport.Total = ordersReport.Items.Sum(i => i.Cost);

        private static void BuildOrdersDictionary(Dictionary<ProductType, double> result, Product p)
        {
            var type = ProductType.SmallParcel;
            var price = 3.0;
            if (p.Dimension < 10) {
                price = p.Quantity * 3;
                price = ChargeExtraWeight(p, price, 1);
            }
            else if (p.Dimension < 50) {
                type = ProductType.MediumParcel;
                price = p.Quantity * 8;
                price = ChargeExtraWeight(p, price, 3);
            }
            else if (p.Dimension < 100) {
                type = ProductType.LargeParcel;
                price = p.Quantity * 15;
                price = ChargeExtraWeight(p, price, 6);
            }
            else if (p.Dimension >= 100) {
                type = ProductType.XlParcel;
                price = p.Quantity * 25;
                price = ChargeExtraWeight(p, price, 10);
            }

            if (result.Keys.Contains(type))
                result[type] += price;
            else
                result.Add(type, price);
        }

        private static double ChargeExtraWeight(Product p, double price, double weightLimit)
        {
            if (p.WeightPerItem > weightLimit)
                price += p.WeightPerItem * p.Quantity * 2;
            return price;
        }
    }
}