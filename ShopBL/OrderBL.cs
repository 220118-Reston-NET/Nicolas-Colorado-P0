using ShopDL;
using ShopModel;

namespace ShopBL
{
    public class OrderBL : IOrderBL
    {
        //Dependency injection Pattern
        //This will save time on rewriting code without compiler helping
        private IRepository _repo;
        public OrderBL(IRepository p_repo)
        {
            _repo = p_repo;
        }
        //====================================


        public void PlaceNewOrder(int p_customerID, int p_storeID, double p_priceTotal, List<LineItem> p_orderedItems)
        {
            _repo.PlaceNewOrder(p_customerID, p_storeID, p_priceTotal, p_orderedItems);
        }
    }
}