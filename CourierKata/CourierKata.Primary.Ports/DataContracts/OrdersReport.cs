using System.Collections;
using System.Collections.Generic;

namespace CourierKata.Primary.Ports.DataContracts
{
    public class OrdersReport
    {
        public ICollection<OrderItem> Items { get; set; }

        public double Total { get; set; }
    }
}