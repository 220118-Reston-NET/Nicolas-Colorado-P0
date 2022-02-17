using ShopModel;

namespace ShopBL
{
    public interface IOrderBL
    {
        /// <summary>
        /// Will allow customers to place orders.
        /// </summary>
        /// <param name="p_customerID"></param>
        /// <param name="p_storeID"></param>
        /// /// <param name="p_priceTotal"></param>
        /// /// <param name="p_orderedItems"></param>
        /// <returns> Returns an order, total price, and update to the inventory. </returns>
        void PlaceNewOrder(int p_customerID, int p_storeID, double p_priceTotal, List<LineItem> p_orderedItems);
    }
}