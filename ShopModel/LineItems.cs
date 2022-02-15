namespace ShopModel
{
    public class LineItem
    {
        public int orderID { get; set; }

        public int productID { get; set; }
        
        public string ProductName { get; set; }

        public int Quantity { get; set; }


        public override string ToString()
        {
            return $"{ProductName}\nQTY: {Quantity}";
        }
    }
}