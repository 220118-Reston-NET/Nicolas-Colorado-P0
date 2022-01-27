using ShopModel;

namespace ShopDL
{
    //This is the Data Layer responsible for interacting with our database
    //CRUD = Create, read, Update, Delete
    public interface IRepository
    {
        //Add a customer to the database
        Customer AddCustomerMenu(Customer p_customer);

        //Will give all customer in the database
        //return a list of collection
        List<Customer> GetAllCustomer();

    }
}
