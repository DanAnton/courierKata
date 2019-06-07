namespace CourierKata.Primary.Ports.DataContracts
{
    public class Product
    {
        public double Dimension { get; set; }
        public int Quantity { get; set; }
        public double WeightPerItem { get; set; }
    }

    public enum ProductType
    {
        SmallParcel,
        MediumParcel,
        LargeParcel,
        XlParcel
    }
}