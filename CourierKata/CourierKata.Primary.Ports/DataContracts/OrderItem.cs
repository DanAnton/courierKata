namespace CourierKata.Primary.Ports.DataContracts
{
    public class OrderItem
    {
        public OrderItemType Type { get; set; }

        public double Cost { get; set; }

        public int Quantity { get; set; }
    }

    public enum OrderItemType
    {
        SmallParcel,
        MediumParcel,
        LargeParcel,
        XlParcel,
        HeavyParcel,
        FastShipping,
        SmallParcelMania,
        MediumParcelMania,
        MixedParcelMania
    }
}