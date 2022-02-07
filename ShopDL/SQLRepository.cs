using ShopModel;

namespace ShopDL
{
    public class SQLRepository : IRepository
    {
        //SQLRepsitory point to connection string belonging to an existing database to create an object out of it
        //It would also allow SQLRepository to point to different databases as long as you have the connection string
        private readonly string _connectionStrings;
        public SQLRepository(string p_connectionStrings)
        {
            _connectionStrings = p_connectionStrings;
        }

        public Customer AddCustomerMenu(Customer p_customer)
        {
            string sqlQuery = @"insert into Customer
                            values(@Name, @Address, @Email, @Phone)";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
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


        public List<Orders> GetOrderbyCustomerID(int p_customerID)
        {
            List<Orders> listofOrders = new List<Orders>();

            string sqlQuery = @"select o.orderID, o.storeID, o.StoreFrontLocation, o.TotalPrice from Customer c 
                            inner join ViewOrder vo on c.customerID = vo.customerID 
                            inner join Orders o on o.orderID = vo.orderID
                            where c.customerID = @customerID";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@customerID", p_customerID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofOrders.Add(new Orders()
                    {
                        orderID = reader.GetInt32(0),
                        storeID = reader.GetInt32(1),
                        Location = reader.GetInt32(2),
                        TotalPrice = reader.GetInt32(3)
                    });
                }
            }
            return p_customerID;
        }


        public List<Customer> GetAllCustomer()
        {
            List<AddCustomerMenu> listofCustomer = new List<Customer>();

            string sqlQuery = @"select * from Customer";

            using (SqlConnection co = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofCustomer.Add(new Customer()
                    {
                        customerID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Orders = reader.GetOrderbyCustomerID(reader.GetInt32(0))
                    });
                }
            }
            return listofCustomer;
        }


        public List<Orders> GetOrderbyStoreID(int p_storeID)
        {
            List<Orders> listofOrders = new List<Orders>();

            string sqlQuery = @"select o.orderID, o.customerID, o.StoreFrontLocation, o.TotalPrice from StoreFront sf 
                            inner join ViewStoreOrder vso on sf.storeID = vso.storeID 
                            inner join Orders o on o.orderID = vso.orderID";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@storeID", p_storeID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofOrders.Add(new Orders()
                    {
                        orderID = reader.GetInt32(0),
                        customerID = reader.GetInt32(1),
                        Location = reader.GetInt32(2),
                        TotalPrice = reader.GetInt32(3)
                    });
                }
            }
            return p_storeID;
        }


        public List<Product> GetProductbyStoreID(int p_storeID)
        {
            List<Product> listofProducts = new List<Product>();

            string sqlQuery = @"select sf.storeID, p.productID, p.Name, i.Quantity from StoreFront sf 
                            inner join Inventory i on sf.storeID = i.storeID
                            inner join Product p on p.productID = i.productID";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@storeID", p_storeID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofProducts.Add(new Product()
                    {
                        storeID = reader.GetInt32(0),
                        productID = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Quantity = reader.GetInt32(3)
                    });
                }
            }
            return p_storeID;
        }
        

        public List<StoreFront> GetAllStoreFront()
        {

            string sqlQuery = @"select * from StoreFront";

            using (SqlConnection co = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofStoreFront.Add(new StoreFront()
                    {
                        storeID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Phone = reader.GetString(3),
                        Products = reader.GetProductbyStoreID(reader.GetInt32(0)),
                        Orders = reader.GetOrderbyCustomerID(reader.GetInt32(0))
                    });
                }
            }
            return listofStoreFront;
        }
    }
}