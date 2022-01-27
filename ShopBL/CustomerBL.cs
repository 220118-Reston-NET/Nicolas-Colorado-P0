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

        public Customer AddCustomerMenu(Customer p_customer)
        {
            return _repo.AddCustomerMenu(p_customer);
        }

        public List<Customer> SearchCustomerMenu(string p_name)
        {
            List<Customer> listofCustomer = _repo.GetAllCustomer();

            //LINQ library
            return listofCustomer
                        .Where(customer => customer.Name.Contains(p_name))
                        .ToList();
        }
    }
}
