namespace ShopModel
{
    public class LineItem
    {
        private List<Product> _product;
        public List<Product> Product
        {
            get { return _product; }
            set
            {
                _product = value;

            }
            
        }
        public int Quantity { get; set; }

    }
}