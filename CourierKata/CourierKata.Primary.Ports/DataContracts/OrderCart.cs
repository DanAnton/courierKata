using System.Collections.Generic;

namespace CourierKata.Primary.Ports.DataContracts
{
    public class OrderCart
    {
        public IList<Product> Products { get; set; }
    }
}