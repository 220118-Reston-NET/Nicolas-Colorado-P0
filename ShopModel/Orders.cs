namespace ShopModel
{
    public class Orders
    {
        public int orderID { get; set; }

        public int customerID { get; set; }

        public int storeID { get; set; }

        private List<LineItem> _lineItem;

        public List<LineItem> LineItem
        {
            get { return _lineItem; }
            set
            {
                _lineItem = value;
            }
        }

        public string StoreFrontLocation { get; set; }

        public double TotalPrice { get; set; }

        public override string ToString()
        {
            return $"OrderID: {orderID}\nStoreID: {storeID}\nLocation: {StoreFrontLocation}\nTotal Price: {TotalPrice}";
        }
    }
}