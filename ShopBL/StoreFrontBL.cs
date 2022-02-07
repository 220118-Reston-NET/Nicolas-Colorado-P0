using ShopDL;
using ShopModel;

namespace ShopBL
{
    public class StoreFrontBL : IStoreFrontBL
    {
        //Dependency injection Pattern
        //This will save time on rewriting code without compiler helping
        private IRepository _repo;
        public StoreFrontBL(IRepository p_repo)
        {
            _repo = p_repo;
        }
        //====================================

        public List<Orders> GetOrderbyStoreID(int p_storeID)
        {
            return _repo.GetOrderbyStoreID(p_storeID);
        }

        public List<StoreFront> GetAllStoreFront()
        {
            return _repo.GetAllStoreFront();
        }
    }
}