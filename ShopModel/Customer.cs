namespace ShopModel
{
    public class Customer
    {   
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone 
        { 
            get; 

            set
            {
                Phone.ToString().Length;
                //Cannot have more than 10 digits on a phone number
                if (Phone.ToString().Length < 11)
                {
                    return set;
                }
                else 
                {
                    throw new Exception("Customer's phone number cannot have more than 10 digits!");
                }
            } 
        }

        public List<Order> Orders
        {
            
        }


    }
}

