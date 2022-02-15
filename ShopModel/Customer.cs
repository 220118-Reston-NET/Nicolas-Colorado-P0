namespace ShopModel
{
    public class Customer
    {   
        public int customerID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        private string _phone;
        public string Phone 
        { 
            get { return _phone; }
            set
            {
                //Cannot have more than 10 digits on a phone number
                int v = _phone.Length;
                if (v == 10)
                {
                    _phone = value;
                }
                else 
                {
                    throw new Exception("Customer's phone number cannot have more or less than 10 digits!");
                }
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

        public Customer()
        {
            Name = "";
            Address = "";
            Email = "";
            _phone = "          ";
            _orders = new List<Orders>()
            {
                new Orders()
            };
        }

        //String version of the object
        public override string ToString()
        {
            return $"ID: {customerID}\nName: {Name}\nAddress: {Address}\nEmail: {Email}\nPhone: +1 {Phone}";
        }
    }
}

