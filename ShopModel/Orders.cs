namespace ShopModel
{
    public class Order
    {
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
        public double Price { get; set; }

    }
}