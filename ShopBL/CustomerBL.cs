using ShopDL;
using ShopModel;


namespace ShopBL
{
    public class CustomerBL : ICustomerBL
    {
        //Dependency injection Pattern
        //This will save time on rewriting code without compiler helping
        private IRepository _repo;
        public CustomerBL(IRepository p_repo)
        {
            _repo = p_repo;
        }
        //====================================


        public Customer AddCustomer(Customer p_customer)
        {
            return _repo.AddCustomer(p_customer);
        }


        public List<Orders> GetOrderbyCustomerID(int p_customerID)
        {
            return _repo.GetOrderbyCustomerID(p_customerID);
        }

        public List<Customer> GetCustomerbyCustomerID(int p_customerID)
        {
            return _repo.GetCustomerbyCustomerID(p_customerID);
        }


        public List<Customer> GetAllCustomer()
        {
            return _repo.GetAllCustomer();
        }


        public List<Customer> SearchCustomer(string c_search, string c_name)
        {
            List<Customer> listCustomer = _repo.GetAllCustomer();

            switch (c_search)
            {
                //Use the LINQ library to validate the SearchCustomer function
                case "1":
                    var searchname = listCustomer.Find(c => c.Name.Contains(c_name));
                    if (searchname != null)
                    {
                        return listCustomer
                                    .Where(customer => customer.Name.Contains(c_name))
                                    .ToList();
                    }
                    else 
                    {
                        throw new Exception("This customer name cannot be found.");
                    }
                case "2":
                    var searchemail = listCustomer.Find(c => c.Email == c_name);
                    if (searchemail != null)
                    {
                        return listCustomer
                                    .Where(customer => customer.Email.Equals(c_name))
                                    .ToList();
                    }
                    else 
                    {
                        throw new Exception("This customer email cannot be found.");
                    }
                case "3":
                    var searchphone = listCustomer.Find(c => c.Phone == c_name);
                    if (searchphone != null)
                    {
                        return listCustomer
                                    .Where(customer => customer.Phone.Equals(c_name))
                                    .ToList();
                    }
                    else 
                    {
                        throw new Exception("This customer phone number cannot be found.");
                    }
                default:
                    Console.WriteLine("Customer information could not be found. Press the the Enter key to continue.");
                    Console.ReadLine();
                    return listCustomer;
            }
        }
    }
}
