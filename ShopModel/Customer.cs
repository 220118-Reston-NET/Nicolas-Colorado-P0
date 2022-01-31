namespace ShopModel
{
    public class Customer
    {   
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        private double _phone;
        public double Phone 
        { 
            get { return _phone; }
            set
            {

                //Cannot have more than 10 digits on a phone number
                if (_phone.Count() == 10)
                {
                    return set;
                }
                else 
                {
                    throw new Exception("Customer's phone number cannot have more than 10 digits!");
                }

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

        //String version of the object
        public override string ToString()
        {
            return $"Name: {Name}\nAddress: {Address}\nEmail: {Email}\nPhone: +1 {_phone}\nCurrent Orders: {_order}";
        }

    }
}

