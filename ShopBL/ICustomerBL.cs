using ShopModel;

namespace ShopBL
{
    //Business Layer is reponsible for furtehr validation of data
    //obtained from database or user
    public interface ICustomerBL
    {
        //Adds customers to the database
        Customer AddCustomer(Customer p_customer);
    }
}