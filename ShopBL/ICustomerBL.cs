using ShopModel;

namespace ShopBL
{
    //Business Layer is reponsible for furtehr validation of data
    //obtained from database or user
    public interface ICustomerBL
    {
        /// <summary>
        /// Will add customer infofrmation to the database.
        /// </summary>
        /// <param name="p_customer"></param>
        /// <returns>Returns a customer's information</returns>
        Customer AddCustomer(Customer p_customer);

        /// <summary>
        /// Will search for customer in the listed database based on search parameters.
        /// </summary>
        /// <param name="c_search">This is whatever search parameter the user chose.</param>
        /// <param name="c_name">The parameter holding the customer information.</param>
        /// <returns> Returns a list of customer information based on search. </returns>
        List<Customer> SearchCustomer(string c_search, string c_name);

        /// <summary>
        /// Will give back all customers in the database in the form of a list.
        /// </summary>
        /// <returns> Returns a list collection of all customers in database. </returns>
        List<Customer> GetAllCustomer();

        /// <summary>
        /// Will give back a list of orders by customer (There's only one currently).
        /// </summary>
        /// <param name="p_customerID"></param>
        /// <returns> Returns a list collection of order objects. </returns>
        List<Orders> GetOrderbyCustomerID(int p_customerID);

        /// <summary>
        /// Gets back a customer from a customer ID input.
        /// </summary>
        /// <param name="p_customerID"></param>
        /// <returns> Returns customer that was added. </returns>
        List<Customer> GetCustomerbyCustomerID(int p_customerID);

    }
}