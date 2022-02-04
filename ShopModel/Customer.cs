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
                if (_phone.Length() == 10)
                {
                    return set;
                }
                else 
                {
                    throw new Exception("Customer's phone number cannot have more than 10 digits!");
                }

            } 
        }
        private List<Orders> _orders;
        public List<Orders> Orders
        {
            get { return _orders; }
            set 
            {
                value = _orders;
            }
        }

        //String version of the object
        public override string ToString()
        {
            return $"Name: {Name}\nAddress: {Address}\nEmail: {Email}\nPhone: +1 {_phone}\nCurrent Orders: {_orders}";
        }

    }
}

