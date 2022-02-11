using System.Data.SqlClient;
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


        public Customer AddCustomer(Customer p_customer)
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
                        //reader column is not based on table structure but on your select query statement is displaying
                        orderID = reader.GetInt32(0),
                        storeID = reader.GetInt32(1),
                        StoreFrontLocation = reader.GetString(2),
                        TotalPrice = reader.GetDouble(3)
                    });
                }
            }
            return listofOrders;
        }

        public List<Customer> GetAllCustomer()
        {
            List<Customer> listofCustomer = new List<Customer>();

            string sqlQuery = @"select * from Customer";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofCustomer.Add(new Customer()
                    {
                        //Zero-based column index
                        customerID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Orders = GetOrderbyCustomerID(reader.GetInt32(0))
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
                            inner join Orders o on o.orderID = vso.orderID
                            where sf.storeID = @storedID";
            
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
                        StoreFrontLocation = reader.GetString(2),
                        TotalPrice = reader.GetDouble(3)
                    });
                }
            }
            return listofOrders;
        }


        public List<Product> GetProductbyStoreID(int p_storeID)
        {
            List<Product> listofProducts = new List<Product>();

            string sqlQuery = @"select p.productID, p.Name, p.Price, p.Category, i.Quantity from Product p
                            inner join Inventory i on i.productID = p.productID
                            inner join StoreFront on sf.storeID = i.productID
                            where sf.storeID = @storeID";
            
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
                        productID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDouble(2),
                        Category = reader.GetString(3),
                        Quantity = reader.GetInt32(4)
                    });
                }
            }
            return listofProducts;
        }
        

        public List<StoreFront> GetAllStoreFront()
        {
            List<StoreFront> listofStoreFront = new List<StoreFront>();

            string sqlQuery = @"select * from StoreFront";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
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
                        Product = GetProductbyStoreID(reader.GetInt32(0)),
                        Orders = GetOrderbyCustomerID(reader.GetInt32(0))
                    });
                }
            }
            return listofStoreFront;
        }

        public void ReplenishInventory(int p_productID, int p_Quantity)
        {
            List<StoreFront> listofStoreFront = new List<StoreFront>();

            int addQuantity = 0;

            string sqlQuery = @"select i.Quantity from Inventory i
                            where i.productID = @productID";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@productID", p_productID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    addQuantity = reader.GetInt32(0);
                }
                addQuantity = addQuantity + p_Quantity;
            }
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@Quantity", addQuantity);
                command.Parameters.AddWithValue("@productID", p_productID);

                command.ExecuteNonQuery();
            }

        }
    }
}