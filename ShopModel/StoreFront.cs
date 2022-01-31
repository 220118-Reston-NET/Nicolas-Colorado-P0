namespace ShopModel
{
    public class StoreFront
    {

        public string Name { get; set; }
        
        public string Address { get; set; }

        private List<Products> _product;
        public List<Product> Product 
        {
            get { return _product; }
            set 
            { 
                value = _product;
            }
        }

        private List<Order> _order;
        public List<Order> Order
        {
            get { return _order; }
            set 
            { 
                value = _order;
            }
        }

        public StoreFront()
        {
            Name = "Nick's Pawn Shop";
            Address = " 5470 Las Vegas Boulevard, Las Vegas, NV";
            _product = new List<Product>()
            {
                new Product()
            };
            _order = new List<Product>()
            {
                new Order()
            };
        }

        public override string ToString()
        {
            return $"Name: {Name}\nAddress: {Address}\nProducts: {_product}\nCurrent Orders: {_order}";
        }
    }
}