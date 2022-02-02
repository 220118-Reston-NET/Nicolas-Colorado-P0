using ShopModel;

namespace ShopDL
{
    public class SQLRepository : IRepository
    {
        public Customer AddCustomer(Customer p_customer)
        {
            string sqlQuery = @"insert into Customer
                            values(@Name, @Address, @Email, @Phone)";

            using (SqlConnection con = new SqlConnection("Server=tcp:projectshopdb.database.windows.net,1433;Initial Catalog=ShopDB;Persist Security Info=False;User ID=shopAdmin;Password=Ni515028!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@Name", p_customer.Name);
                command.Parameters.AddWithValue("@Address", p_customer.Address);
                command.Parameters.AddWithValue("@Email", p_customer.Email);
                command.Parameters.AddWithValue("@Phone", p_customer.Phone);

                command.ExecuteNonQuery();
            }    

            return p_customer;          
        }

        public List<Customer> GetAllCustomer()
        {
            List<AddCustomerMenu> listofcustomer = new List<Customer>();

            string sqlQuery = @"select * from Customer";

            using (SqlConnection co = new SqlConnection("Server=tcp:projectshopdb.database.windows.net,1433;Initial Catalog=ShopDB;Persist Security Info=False;User ID=shopAdmin;Password=Ni515028!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofcustomer.Add(new Customer()
                    {
                        customerID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4)
                    });
                }
            }

            return listofcustomer;
        }
    }
}