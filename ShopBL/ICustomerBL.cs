using ShopModel;

namespace ShopBL
{
    //Business Layer is reponsible for furtehr validation of data
    //obtained from database or user
    public interface ICustomerBL
    {
        //Adds customers to the database
        Customer AddCustomerMenu(Customer p_customer);

        //Will give list related to searched customer
        List<Customer> SearchCustomerMenu(string p_name);
    }
}