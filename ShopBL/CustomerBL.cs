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


        public List<Customer> GetAllCustomer()
        {
            return _repo.GetAllCustomer();
        }


        public List<Customer> SearchCustomer(string c_search, string c_name)
        {
            List<Customer> listCustomer = _repo.GetAllCustomer();

            switch (c_search)
            {
                case "1":
                //validation process using LINQ Library
                    return listCustomer
                                .Where(customer => customer.Name.Contains(c_name))
                                .ToList();
                case "2":
                //LINQ Library
                    return listCustomer
                                .Where(customer => customer.Email.Contains(c_name))
                                .ToList();
                case "3":
                    return listCustomer
                                .Where(customer => customer.Phone.Contains(c_name))
                                .ToList();
                default:
                    Console.WriteLine("Customer information could not be found. Press the the Enter key to continue.");
                    Console.ReadLine();
                    return listCustomer;
            }
        }
    }
}
