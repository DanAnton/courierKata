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
            ComputeDiscounts(ordersReport);
            RecalculateTotal(ordersReport);
            AddSpeedyShipping(cart.HasFastDeliver, ordersReport);
            RecalculateTotal(ordersReport);
            return ordersReport;
        }

        private static OrdersReport ComputeOrdersReport(OrderCart cart)
        {
            var result = new List<OrderItem>();
            cart.Products.ToList().ForEach(p => ComputeOrdersReportInternal(result, p));

            var ordersReport = new OrdersReport
                               {
                                   Items = result,
                                   Total = result.Sum(r => r.Cost)
                               };
            return ordersReport;
        }

        private static void AddSpeedyShipping(bool hasFastDeliver, OrdersReport ordersReport)
        {
            if (!hasFastDeliver) return;
            ordersReport.Items.Add(new OrderItem
                                   {
                                       Type = OrderItemType.FastShipping,
                                       Cost = ordersReport.Total,
                                       Quantity = 1
                                   });
        }

        private static void RecalculateTotal(OrdersReport ordersReport)
            => ordersReport.Total = ordersReport.Items.Sum(i => i.Cost);

        private static void ComputeOrdersReportInternal(ICollection<OrderItem> orderItems, Product p)
        {
            var type = OrderItemType.SmallParcel;
            var price = 50.0;

            if (p.WeightPerItem > 50) {
                type = OrderItemType.HeavyParcel;
                price = p.Quantity * 50;
                price = ChargeExtraWeight(p, price, 50, 1);
            }
            else if (p.Dimension < 10 && p.WeightPerItem < 50) {
                price = p.Quantity * 3;
                price = ChargeExtraWeight(p, price, 1, 2);
            }
            else if (p.Dimension < 50 && p.WeightPerItem < 50) {
                type = OrderItemType.MediumParcel;
                price = p.Quantity * 8;
                price = ChargeExtraWeight(p, price, 3, 2);
            }
            else if (p.Dimension < 100 && p.WeightPerItem < 50) {
                type = OrderItemType.LargeParcel;
                price = p.Quantity * 15;
                price = ChargeExtraWeight(p, price, 6, 2);
            }
            else if (p.Dimension >= 100 && p.WeightPerItem < 50) {
                type = OrderItemType.XlParcel;
                price = p.Quantity * 25;
                price = ChargeExtraWeight(p, price, 10, 2);
            }

            orderItems.Add(new OrderItem
                           {
                               Type = type,
                               Cost = price,
                               Quantity = p.Quantity
                           });
        }

        private static double ChargeExtraWeight(Product p, double price, double weightLimit, int extraPrice)
        {
            if (p.WeightPerItem > weightLimit)
                price += (p.WeightPerItem - weightLimit) * p.Quantity * extraPrice;
            return price;
        }

        private static void ComputeDiscounts(OrdersReport ordersReport)
        {
            var discounts = new List<OrderItem>();
            if (ordersReport.Items.All(i => i.Type.Equals(OrderItemType.SmallParcel))) {
                var filteredOrdersReport = ordersReport.Items.Where(i => i.Quantity >= 4).ToList();
                ComputeDiscountsInternal(filteredOrdersReport, discounts, 4, OrderItemType.SmallParcelMania);
            }
            else if (ordersReport.Items.All(i => i.Type.Equals(OrderItemType.MediumParcel))) {
                var filteredOrdersReport = ordersReport.Items.Where(i => i.Quantity >= 3).ToList();
                ComputeDiscountsInternal(filteredOrdersReport, discounts, 3, OrderItemType.MediumParcelMania);
            }
            else {
                var filteredOrdersReport = ordersReport
                                           .Items.Where(i => i.Quantity >= 5)
                                           .OrderBy(i => i.Type)
                                           .GroupBy(i => i.Type)
                                           .FirstOrDefault()?.ToList();
                ComputeDiscountsInternal(filteredOrdersReport, discounts, 5, OrderItemType.MixedParcelMania);
            }

            foreach (var discount in discounts) ordersReport.Items.Add(discount);
        }

        private static void ComputeDiscountsInternal(IEnumerable<OrderItem> orderItems,
                                                     ICollection<OrderItem> discounts,
                                                     int nthProduct,
                                                     OrderItemType discountType)
        {
            if (orderItems == null) return;
            foreach (var ordersReportItem in orderItems) {
                var a = (int) Math.Round((decimal) ordersReportItem.Quantity / nthProduct,
                                         MidpointRounding.AwayFromZero);
                if (a < 1) continue;

                switch (ordersReportItem.Type) {
                    case OrderItemType.SmallParcel:
                        discounts.Add(new OrderItem
                                      {
                                          Type = discountType,
                                          Cost = 0 - 3 * a,
                                          Quantity = a
                                      });
                        break;
                    case OrderItemType.MediumParcel:
                        discounts.Add(new OrderItem
                                      {
                                          Type = discountType,
                                          Cost = 0 - 8 * a,
                                          Quantity = a
                                      });
                        break;
                    case OrderItemType.LargeParcel:
                        discounts.Add(new OrderItem
                                      {
                                          Type = discountType,
                                          Cost = 0 - 15 * a,
                                          Quantity = a
                                      });
                        break;
                    case OrderItemType.XlParcel:
                        discounts.Add(new OrderItem
                                      {
                                          Type = discountType,
                                          Cost = 0 - 25 * a,
                                          Quantity = a
                                      });
                        break;
                    default:
                        discounts.Add(new OrderItem
                                      {
                                          Type = discountType,
                                          Cost = 0 - 50 * a,
                                          Quantity = a
                                      });
                        break;
                }
            }
        }
    }
}