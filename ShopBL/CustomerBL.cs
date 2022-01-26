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

        public Customer AddProjectMenu(Customer p_customer)
        {
            return _repo.AddProjectMenu(p_customer);
        }
    }
}
