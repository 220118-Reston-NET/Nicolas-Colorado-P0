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
        Customer AddCustomerMenu(Customer p_customer);

        /// <summary>
        /// Search for customer in database based on search parameters
        /// </summary>
        /// <param name="p_customer"></param>
        /// <returns> Returns customer that was searched for. </returns> 
        Customer SearchCustomerMenu(Customer p_customer);

        /// <summary>
        /// Will give back all customers in the database in the form of a list.
        /// </summary>
        /// <returns> Returns a list collection of all customers in database. </returns>
        List<Customer> GetAllCustomer();

        // <summary>
        /// Will give back all the store fronts in the database (There's only one currently).
        /// </summary>
        /// <returns> Returns all store fronts. </returns>
        List<StoreFront> GetAllStoreFront();

    }
}
