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
                                                     Type = ProductType.SmallParcel.ToString(),
                                                     Cost = 120
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = ProductType.MediumParcel.ToString(),
                                                     Cost = 240
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = ProductType.XlParcel.ToString(),
                                                     Cost = 75
                                                 }
                                             },
                                     Total = 435.0
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 10,
                                                                         Quantity = 30
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 40
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
                                                     Type = ProductType.SmallParcel.ToString(),
                                                     Cost = 120
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = ProductType.MediumParcel.ToString(),
                                                     Cost = 240
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = ProductType.XlParcel.ToString(),
                                                     Cost = 75
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = "Speedy shipping",
                                                     Cost = 435.0
                                                 }
                                             },
                                     Total = 435.0 * 2
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 10,
                                                                         Quantity = 30
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 40
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 100,
                                                                         Quantity = 3
                                                                     }
                                                                 },
                                                      HasSpeedyDeliver = true
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
                                                     Type = ProductType.SmallParcel.ToString(),
                                                     Cost = 120
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = ProductType.MediumParcel.ToString(),
                                                     Cost = 540
                                                 },
                                                 new OrderItem
                                                 {
                                                     Type = ProductType.XlParcel.ToString(),
                                                     Cost = 75
                                                 }
                                             },
                                     Total = 735.0 
                                 };

            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Dimension = 10,
                                                                         Quantity = 30,
                                                                         WeightPerItem = 5
                                                                     },
                                                                     new Product
                                                                     {
                                                                         Dimension = 2.4,
                                                                         Quantity = 40
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