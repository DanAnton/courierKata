using System.Collections.Generic;
using System.Linq;
using CourierKata.Domain.Implementation;
using CourierKata.Primary.Ports.DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            // Act
            var result = _service.GetOrdersReport(new OrderCart
                                                  {
                                                      Products = new List<Product>
                                                                 {
                                                                     new Product
                                                                     {
                                                                         Type = "Small",
                                                                         Quantity = 30
                                                                     }
                                                                 }
                                                  });
            //Assert
            Assert.IsTrue(result.Any());
        }
    }
}