using ShopModel;

namespace ShopBL
{
    public interface IStoreFrontBL
    {
        /// <summary>
        /// Will give back all customers in the database in the form of a list.
        /// </summary>
        /// <returns> Returns a list collection of all customers in database. </returns>
        List<StoreFront> GetAllStoreFront();

        /// <summary>
        /// Will give back a list of orders by customer (There's only one currently).
        /// </summary>
        /// <param name="p_storeID"></param>
        /// <returns> Returns a list collection of order objects. </returns>
        List<Orders> GetOrderbyStoreID(int p_storeID);
    }
}