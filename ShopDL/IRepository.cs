using ShopModel;

namespace ShopDL
{
    //This is the Data Layer responsible for interacting with our database
    //CRUD = Create, read, Update, Delete
    public interface IRepository
    {
        /// <summary>
        /// Add a customer to the database
        /// </summary>
        /// <param name="p_customer"></param>
        /// <returns> Returns customer that was added. </returns>
        Customer AddCustomer(Customer p_customer);

        /// <summary>
        /// Will give back all customers in the database in the form of a list.
        /// </summary>
        /// <returns> Returns a list collection of all customers in database. </returns>
        List<Customer> GetAllCustomer();

        /// <summary>
        /// Gets back a customer from a customer ID input.
        /// </summary>
        /// <param name="p_customerID"></param>
        /// <returns> Returns customer that was added. </returns>
        List<Customer> GetCustomerbyCustomerID(int p_customerID);

        /// <summary>
        /// Will give back a list of orders by customer (There's only one currently).
        /// </summary>
        /// <param name="p_customerID"></param>
        /// <returns> Returns a list collection of order objects. </returns>
        List<Orders> GetOrderbyCustomerID(int p_customerID);

        // <summary>
        /// Will give back all the store fronts in the database (There's only one currently).
        /// </summary>
        /// <returns> Returns all store fronts. </returns>
        List<StoreFront> GetAllStoreFront();

        /// <summary>
        /// Will give back a list of orders by store. (There's only one currently).
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

        // /// <summary>   
        // /// Will give back a list of all products.
        // /// </summary>
        // /// <returns> Returns a list collection of products. </returns>
        // List<Product> GetAllProducts();

        /// <summary>
        /// Will allow store inventory to be replenished.
        /// </summary>
        /// <param name="p_productID"></param>
        /// <param name="p_Quantity"></param>
        /// <param name="p_storeID"></param>
        /// <returns> Product ID and the updated product quantity. </returns>
        void ReplenishInventory(int p_productID, int p_Quantity, int p_storeID);

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
