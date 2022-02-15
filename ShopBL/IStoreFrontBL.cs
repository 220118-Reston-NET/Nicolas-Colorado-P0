using ShopModel;

namespace ShopBL
{
    public interface IStoreFrontBL
    {
        /// <summary>
        /// Will give back all stores in the database in the form of a list.
        /// </summary>
        /// <returns> Returns a list collection of all stores in database. </returns>
        List<StoreFront> GetAllStoreFront();

        /// <summary>
        /// Will give back a list of orders by stores. (There's only one currently).
        /// </summary>
        /// <param name="p_storeID"></param>
        /// <returns> Returns a list collection of order objects. </returns>
        List<Orders> GetOrderbyStoreID(int p_storeID);

        /// <summary>
        /// Will give back a list of products by store. (There's only one currently).
        /// </summary>
        /// <param name="p_storeID"></param>
        /// <returns> Returns a list collection of order objects. </returns>
        List<Product> GetProductbyStoreID(int p_storeID);

        /// <summary>   
        /// Will give back a list of all products.
        /// </summary>
        /// <returns> Returns a list collection of products. </returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Will allow store inventory to be replenished.
        /// </summary>
        /// <param name="p_productID"></param>
        /// <param name="p_Quantity"></param>
        /// <returns> Returns an update to the inventory via product quantity.
        void ReplenishInventory(int p_productID, int p_Quantity);

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