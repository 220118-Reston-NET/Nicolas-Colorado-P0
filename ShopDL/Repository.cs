using System.Text.Json;
using ShopModel;

namespace ShopDL
{
    public class Repository : IRepository
    {
        //Filepath is from ShopUI
        private string _filepath = "../ShopDL/Database/";
        private string _jsonString;
        public Customer AddCustomer(Customer p_customer)
        {
            string path = _filepath + "Customer.json";
            List<Customer>  listofCustomer = new List<Customer>();
            listofCustomer.Add(p_customer);

            _jsonString = JsonSerializer.Serialize(p_customer, new JsonSerializerOptions {WriteIndented = true});

            File.WriteAllText(path , _jsonString);
            
            return p_customer;
        }

        public List<Customer> GetAllCustomer()
        {
            //Grab information from the JSON file and stores it in the string
            _jsonString = File.ReadAllText(_filepath + "Customer.json");
            //Deserialize the jsonString into a List<Customer> object and return it
            JsonSerializer.Deserialize<List<Customer>>(_jsonString);
        }
        
    }
}