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

            string sqlQuery = @"select o.orderID, c.customerId, o.storeID, o.TotalPrice from Orders o  
                            inner join Customer c on o.customerID = c.customerID 
                            where o.customerID = @customerID";
                            
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
                        customerID = reader.GetInt32(1),
                        storeID = reader.GetInt32(2),
                        TotalPrice = reader.GetDouble(3)
                    });
                }
            }
            return listofOrders;
        }


         public List<Customer> GetCustomerbyCustomerID(int p_customerID)
         {
            List<Customer> listofCustomer = new List<Customer>();

            string sqlQuery = @"select c.customerID, c.Name, c.Address, c.Email, c.Phone from Customer c
                            where c.customerID = @customerID";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@customerID", p_customerID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listofCustomer.Add(new Customer()
                    {
                        customerID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4)
                    });
                }
            }
            return listofCustomer;
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

            string sqlQuery = @"select o.orderID, sf.storeID, o.customerID, o.TotalPrice from Orders o  
                            inner join StoreFront sf on o.storeID = sf.storeID
                            where o.storeID = @storeID";
            
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
                        storeID = reader.GetInt32(1),
                        customerID = reader.GetInt32(2),
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
                            inner join StoreFront sf on sf.storeID = i.storeID
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


        public void ReplenishInventory(int p_productID, int p_Quantity, int p_storeID)
        {
            List<StoreFront> listofStoreFront = new List<StoreFront>();

            int addQty = 0;

            string sqlQuery = @"select i.Quantity from Inventory i
                            where i.productID = @productID
                            and i.storeID = @storeID";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@productID", p_productID);
                command.Parameters.AddWithValue("@storeID", p_storeID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    addQty = reader.GetInt32(0);
                }
                addQty = addQty + p_Quantity;
            }

            sqlQuery = @"update Inventory
                        set Quantity = @Quantity
                        where productID = @productID
                        and storeID = @storeID";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@Quantity", addQty);
                command.Parameters.AddWithValue("@productID", p_productID);
                command.Parameters.AddWithValue("@storeID", p_storeID);

                command.ExecuteNonQuery();
            }
        }


        public void PlaceNewOrder(int p_customerID, int p_storeID, double p_priceTotal, List<LineItem> p_orderedItems)
        {
            
            //Creates an order, prepares lineitems, and updates the inventory.
            string sqlQuery = @"insert into Orders
                            values(@TotalPrice, @customerID, @storeID);
                            SELECT SCOPE_IDENTITY();";
            
            string sqlQuery2 = @"insert into LineItem
                            values(@orderID, @productID, @Quantity);";

            string sqlQuery3 = @"update Inventory
                            set Quantity = Quantity - @Quantity
                            where storeID = @storeID
                            AND productID = @productID;";
            
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@TotalPrice", p_priceTotal);
                command.Parameters.AddWithValue("@customerID", p_customerID);
                command.Parameters.AddWithValue("@storeID", p_storeID);
                

                int p_orderID = Convert.ToInt32(command.ExecuteScalar());


                foreach (var item in p_orderedItems)
                {
                    SqlCommand command2 = new SqlCommand(sqlQuery2, con);
                    command2.Parameters.AddWithValue("@orderID", p_orderID);
                    command2.Parameters.AddWithValue("@productID", item.productID);
                    command2.Parameters.AddWithValue("@Quantity", item.Quantity);

                    command2.ExecuteNonQuery();

                    SqlCommand command3 = new SqlCommand(sqlQuery3, con);
                    command3.Parameters.AddWithValue("@storeID", p_storeID);
                    command3.Parameters.AddWithValue("@productID", item.productID);
                    command3.Parameters.AddWithValue("@Quantity", item.Quantity);

                    command3.ExecuteNonQuery();
                }    
            }
        }
        
        // public List<Product> GetAllProducts()
        // {
        //     List<Product> listofProducts = new List<Product>();

        //     string sqlQuery = @"select * from Product";

        //     using (SqlConnection con = new SqlConnection(_connectionStrings))
        //     {
        //         con.Open();

        //         SqlCommand command = new SqlCommand(sqlQuery, con);
                
        //         SqlDataReader reader = command.ExecuteReader();

        //         while (reader.Read())
        //         {
        //             listofProducts.Add(new Product()
        //             {
        //                 productID = reader.GetInt32(0),
        //                 Name = reader.GetString(1),
        //                 Price = reader.GetDouble(2),
        //                 Category = reader.GetString(3),
        //                 Quantity = reader.GetInt32(4)
        //             });
        //         }
        //     }
        //     return listofProducts;
        // }
    }
}