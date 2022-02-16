namespace ShopModel
{
    public class StoreFront
    {
        public int storeID { get; set; }

        public string Name { get; set; }
        
        public string Address { get; set; }

        public string Phone { get; set; }

        private List<Product> _product;
        public List<Product> Product 
        {
            get { return _product; }
            set 
            { 
                _product = value;
            }
        }

        private List<Orders> _orders;
        public List<Orders> Orders
        {
            get { return _orders; }
            set 
            { 
                _orders = value;
            }
        }

        public StoreFront()
        {
            Name = "Nick's Pawn Shop";
            Address = "5470 Las Vegas Boulevard, Las Vegas, NV";
            Phone = "429-009-PAWN";
            _product = new List<Product>()
            {
                new Product()
            };
            _orders = new List<Orders>()
            {
                new Orders()
            };
        }

        public override string ToString()
        {
            return $"ID: {storeID}\nName: {Name}\nAddress: {Address}\nPhone: {Phone}";
        }
    }
}