namespace ShopModel
{
    public class LineItem
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{ProductName}\nQTY: {Quantity}";
        }
    }
}