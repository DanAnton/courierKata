using System.Collections.Generic;
using System.Linq;
using CourierKata.Domain.Implementation;
using CourierKata.Primary.Ports.DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CourierKata.Tests
{
    [TestClass]
    public class OrdersServiceTest : BaseTests<OrderCart>
    {
        private OrdersService _service;

        [TestInitialize]
        public override void Initialize()
            => _service = new OrdersService();

        [TestMethod]
        public void GivenOrderItems_GetOrdersReport_ThenShouldReturnOrdersReport()
        {
            //Arrange
            var expectedResult = new OrdersReport
                                 {
                                     Items = new List<OrderItem>
                                             {
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcel,
                                                     Cost = 12,
                                                     Quantity = 4
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcel,
                                                     Cost = 24,
                                                     Quantity = 3
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.XlParcel,
                                                     Cost = 75,
                                                     Quantity = 3
                                                 }
                                             },
                                     Total = 111
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 10,
                                                                         Quantity = 3
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 4
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 100,
                                                                         Quantity = 3
                                                                     }
                                                                 }
                                                  });
            //Assert
            Assert.IsTrue(JsonConvert.SerializeObject(result.Items.OrderBy(r => r.Type).ToList())
                                     .Equals(JsonConvert.SerializeObject(expectedResult.Items.OrderBy(r => r.Type))) &&
                          result.Total.Equals(expectedResult.Total));
        }

        [TestMethod]
        public void GivenOrderItems_GetOrdersReportWithSpeedyShipping_ThenShouldReturnOrdersReportWithSpeedyShipping()
        {
            //Arrange
            var expectedResult = new OrdersReport
                                 {
                                     Items = new List<OrderItem>
                                             {
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcel,
                                                     Cost = 9,
                                                     Quantity = 3
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcel,
                                                     Cost = 8,
                                                     Quantity = 1
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.XlParcel,
                                                     Cost = 75,
                                                     Quantity = 3
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.FastShipping,
                                                     Cost = 92,
                                                     Quantity = 1
                                                 }
                                             },
                                     Total = 184
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 10,
                                                                         Quantity = 1
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 3
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 100,
                                                                         Quantity = 3
                                                                     }
                                                                 },
                                                      HasFastDeliver = true
                                                  });
            //Assert
            Assert.IsTrue(JsonConvert.SerializeObject(result.Items.OrderBy(r => r.Type).ToList())
                                     .Equals(JsonConvert.SerializeObject(expectedResult.Items.OrderBy(r => r.Type))) &&
                          result.Total.Equals(expectedResult.Total));
        }

        [TestMethod]
        public void GivenOrderItems_GetOrdersReportWithExtraWeight_ThenShouldReturnOrdersReportWithExtraWeight()
        {
            //Arrange
            var expectedResult = new OrdersReport
                                 {
                                     Items = new List<OrderItem>
                                             {
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcel,
                                                     Cost = 6,
                                                     Quantity = 2
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcel,
                                                     Cost = 24,
                                                     Quantity = 2
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.XlParcel,
                                                     Cost = 75,
                                                     Quantity = 3
                                                 }
                                             },
                                     Total = 105
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 10,
                                                                         Quantity = 2,
                                                                         WeightPerItem = 5
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 2
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 100,
                                                                         Quantity = 3
                                                                     }
                                                                 }
                                                  });
            //Assert
            Assert.IsTrue(JsonConvert.SerializeObject(result.Items.OrderBy(r => r.Type).ToList())
                                     .Equals(JsonConvert.SerializeObject(expectedResult.Items.OrderBy(r => r.Type))) &&
                          result.Total.Equals(expectedResult.Total));
        }

        [TestMethod]
        public void GivenOrderItems_GetOrdersReportWithHeavyProduct_ThenShouldReturnOrdersReportWithHeavyProduct()
        {
            //Arrange
            var expectedResult = new OrdersReport
                                 {
                                     Items = new List<OrderItem>
                                             {
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcel,
                                                     Cost = 12,
                                                     Quantity = 4
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.HeavyParcel,
                                                     Cost = 104,
                                                     Quantity = 2
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.XlParcel,
                                                     Cost = 75,
                                                     Quantity = 3
                                                 }
                                             },
                                     Total = 191
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 10,
                                                                         Quantity = 2,
                                                                         WeightPerItem = 52
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 4
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 100,
                                                                         Quantity = 3
                                                                     }
                                                                 }
                                                  });
            //Assert
            Assert.IsTrue(JsonConvert.SerializeObject(result.Items.OrderBy(r => r.Type).ToList())
                                     .Equals(JsonConvert.SerializeObject(expectedResult.Items.OrderBy(r => r.Type))) &&
                          result.Total.Equals(expectedResult.Total));
        }

        [TestMethod]
        public void
            GivenOrderItems_GetOrdersReportWithSmallParcelDiscount_ThenShouldReturnOrdersReportWithSmallParcelDiscount()
        {
            //Arrange
            var expectedResult = new OrdersReport
                                 {
                                     Items = new List<OrderItem>
                                             {
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcel,
                                                     Cost = 21,
                                                     Quantity = 3
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcel,
                                                     Cost = 12,
                                                     Quantity = 4
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcel,
                                                     Cost = 27,
                                                     Quantity = 3
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcelMania,
                                                     Cost = -3,
                                                     Quantity = 1
                                                 }
                                             },
                                     Total = 57
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 3.6,
                                                                         Quantity = 3,
                                                                         WeightPerItem = 3
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 4,
                                                                         WeightPerItem = 1
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 5.8,
                                                                         Quantity = 3,
                                                                         WeightPerItem = 4
                                                                     }
                                                                 }
                                                  });
            //Assert
            Assert.IsTrue(JsonConvert.SerializeObject(result.Items.OrderBy(r => r.Type).ToList())
                                     .Equals(JsonConvert.SerializeObject(expectedResult.Items.OrderBy(r => r.Type))) &&
                          result.Total.Equals(expectedResult.Total));
        }

        [TestMethod]
        public void
            GivenOrderItems_GetOrdersReportWithMediumParcelDiscount_ThenShouldReturnOrdersReportWithMediumParcelDiscount()
        {
            //Arrange
            var expectedResult = new OrdersReport
                                 {
                                     Items = new List<OrderItem>
                                             {
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcel,
                                                     Cost = 30,
                                                     Quantity = 3
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcel,
                                                     Cost = 32,
                                                     Quantity = 4
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcel,
                                                     Cost = 42,
                                                     Quantity = 3
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcelMania,
                                                     Cost = -8,
                                                     Quantity = 1
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcelMania,
                                                     Cost = -8,
                                                     Quantity = 1
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcelMania,
                                                     Cost = -8,
                                                     Quantity = 1
                                                 }
                                             },
                                     Total = 80
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 11,
                                                                         Quantity = 3,
                                                                         WeightPerItem = 4
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 12,
                                                                         Quantity = 4,
                                                                         WeightPerItem = 3
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 13,
                                                                         Quantity = 3,
                                                                         WeightPerItem = 6
                                                                     }
                                                                 }
                                                  });
            //Assert
            Assert.IsTrue(JsonConvert.SerializeObject(result.Items.OrderBy(r => r.Type).ToList())
                                     .Equals(JsonConvert.SerializeObject(expectedResult.Items.OrderBy(r => r.Type))) &&
                          result.Total.Equals(expectedResult.Total));
        }

        [TestMethod]
        public void
            GivenOrderItems_GetOrdersReportWithMixedParcelDiscount_ThenShouldReturnOrdersReportMixedParcelDiscount()
        {
            //Arrange
            var expectedResult = new OrdersReport
                                 {
                                     Items = new List<OrderItem>
                                             {
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.SmallParcel,
                                                     Cost = 15,
                                                     Quantity = 5
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MediumParcel,
                                                     Cost = 40,
                                                     Quantity = 5
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.XlParcel,
                                                     Cost = 75,
                                                     Quantity = 3
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = OrderItemType.MixedParcelMania,
                                                     Cost = -3,
                                                     Quantity = 1
                                                 }
                                             },
                                     Total = 127
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 10,
                                                                         Quantity = 5
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 5
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 100,
                                                                         Quantity = 3
                                                                     }
                                                                 }
                                                  });
            //Assert
            Assert.IsTrue(JsonConvert.SerializeObject(result.Items.OrderBy(r => r.Type).ToList())
                                     .Equals(JsonConvert.SerializeObject(expectedResult.Items.OrderBy(r => r.Type))) &&
                          result.Total.Equals(expectedResult.Total));
        }
    }
}