using ShopModel;

namespace ShopBL
{
    //Business Layer is reponsible for furtehr validation of data
    //obtained from database or user
    public interface ICustomerBL
    {
        /// <summary>
        /// Will add customer information to the database
        /// </summary>
        /// <param name="p_customer">this is the customer information</param>
        /// <returns> Returns customer info. </returns>
        Customer AddCustomerMenu(Customer p_customer);

        /// <summary>
        /// Will search for customer in the listed database based on search parameters.
        /// </summary>
        /// <param name="p_name">this is the customer information</param>
        /// <returns> Returns a list of customer information based on search. </returns>
        List<Customer> SearchCustomer(string p_name);
    }
}