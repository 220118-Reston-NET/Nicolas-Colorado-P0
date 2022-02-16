using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class PlaceOrder : IMenu
    {
        private List<Customer> _listofCustomer;
        private List<StoreFront> _listofStoreFront;
    
        //Dependency Injection
        private ICustomerBL _customerBL;
        private IStoreFrontBL _storeBL;
        public PlaceOrder(ICustomerBL p_customerBL, IStoreFrontBL p_storeBL)
        {
            _customerBL = p_customerBL;
            _storeBL = p_storeBL;
            _listofCustomer = _customerBL.GetAllCustomer();
            _listofStoreFront = _storeBL.GetAllStoreFront();
        }
        private static List<LineItem> orderedItems = new List<LineItem>();
        private static List<Product> listOfProducts = new List<Product>();
        public static int _customerID;
        public static int _storeID;
        public static int prodID;
        public static int qty;


        public void Display()
        {
            //The menu that allows user to place on order.
            Console.WriteLine("Welcome to the Colorado's Market Order Menu. To begin your order, you must enter your customer ID on file");
            Console.WriteLine("as well as the ID of the store you want to purchase from.\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - Place an order");
            Console.WriteLine("[2] - Return to the Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    //Displays customers in the database.
                    Log.Information("User selected to place an order.");
                    Console.WriteLine("=============== Customer List ===============");
                    foreach (var item in _listofCustomer)
                    {
                        Console.WriteLine("-------------------------");
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Please enter a customer ID:");
                    try
                    {
                        //Get the customer ID from the user.
                        _customerID = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User has entered a customer ID.");
                        if ((_listofCustomer.All(p => p.customerID != _customerID)))
                        {
                            throw new Exception("Customer ID cannot be found.");
                        }
                    }
                    catch (System.Exception)
                    {
                        Log.Warning("Customer ID could not be found in database.");
                        Console.Write("You've selected an invalid value.");
                        Console.WriteLine("Please press the Enter key to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                        return "PlaceOrder";
                    }
                    //Displays the store list from the database.
                    Console.WriteLine("=============== Store List ===============");
                    foreach (var item in _listofStoreFront)
                    {
                        Console.WriteLine("-------------------------");
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Please enter the store's ID:");
                    try
                    {
                        //Get the store ID.
                        _storeID = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User has entered a store ID.");
                        if ((_listofStoreFront.All(p => p.storeID != _storeID)))
                        {
                            throw new Exception("Store ID could not be found.");
                        }
                    }
                    catch (System.Exception)
                    {
                        Log.Warning("Store ID could not be found in database.");
                        Console.Write("You've selected an invalid value.");
                        Console.WriteLine("Press the Enter button to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                        return "Place Order"; 
                    }
                    try
                    {
                        //Displays the inventory in the store selected by the user.
                        List<Product> listofProducts = _storeBL.GetProductbyStoreID(_storeID);
                        Console.WriteLine("=============== Inventory ===============");
                        foreach (var product in listofProducts)
                        {
                            Console.WriteLine("-------------------------");
                            Console.WriteLine(product);                        
                        }
                        Log.Information("Successfully retrieved and displayed current inventory in a store.");
                    }
                    catch (System.Exception)
                    {
                        Log.Warning("Products could not be found in store.");
                        Console.Write("There are currently no products in this store.");
                        Console.WriteLine("Press the Enter button to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                        return "Place Order"; 
                    }
                    bool shoploop = true;
                    while (shoploop)
                    {
                        //The order menu is stored in a while-loop to allow users to add products to their order repeatedly before checking out.
                        Console.WriteLine("To add product to your order, you must enter the product's ID. What would you like to do next?");
                        Console.WriteLine("[1] - Add a product to an order");
                        Console.WriteLine("[2] - Check out");
                        Console.WriteLine("[3] - Cancel my order");
                        string orderchoice = Console.ReadLine();

                        if (orderchoice == "1")
                        {
                            Log.Information("User selected to add product to an order.");

                            //Products' information  (ID and quantity) are specified by the user to add to the order.
                            try
                            {
                                Console.WriteLine("Please enter product ID:");
                                prodID = Convert.ToInt32(Console.ReadLine());
                                Log.Information("User has entered a product ID.");

                                Console.WriteLine("Now, enter the amount of the product you wish to order:");
                                qty = Convert.ToInt32(Console.ReadLine());
                                Log.Information("User has entered a product qty.");

                                orderedItems.Add(new LineItem()
                                {
                                    productID = prodID,
                                    Quantity = qty
                                });
                            }
                            catch (FormatException)
                            {
                                Log.Warning("User inputted a invalid value for prodID or qty.");
                                Console.WriteLine("You've selected invalid values for prodID and/or qty.");
                                Console.WriteLine("Press the Enter button to try again:");
                                Console.ReadLine();
                                Log.Information("User pressed the Enter key to try again.");
                            }

                            //Makes sure the items ordered don't exceed the quantity of products in the inventory.
                            //Inventory object stores quantity of each product
                            try
                            {
                                int inventory = 0;
                                int orderItemQty = qty;
                                List<Product> listofProducts = _storeBL.GetProductbyStoreID(_storeID);
                                foreach (var item in listofProducts)
                                {
                                    inventory = item.Quantity;
                                }

                                if (orderItemQty <= inventory)
                                {
                                    Console.WriteLine("Product(s) has been added to your order!\n");
                                    Console.WriteLine("Press the enter key to continue:");
                                    Console.ReadLine();
                                    Log.Information("User pressed the Enter key to continue.");
                                }
                                else
                                {
                                    throw new Exception("Products in order exceed what's in the inventory!");
                                }
                            }
                            catch (System.Exception)
                            {
                                Log.Warning("User entered more products than what's currently stored in the inventory.");
                                Console.WriteLine("You cannot order more products than the inventory holds!");
                                Console.WriteLine("Press the the Enter key to try again:");
                                Console.ReadLine();
                                Log.Information("User pressed the Enter key to try again.");
                            }
                        }
                        else if (orderchoice == "2")
                        {
                            Log.Information("User selected to check out an order.");

                            //Break the order menu loop to finally check out with the products.
                            shoploop = false;
                            double priceTotal = 0;
                            Console.WriteLine("Order has been checked out!\n");

                            Product _product = new Product();
                            foreach (var items in orderedItems)
                            {
                                //Create a list storing order items into an updated list.
                                //Using the variable stored above, Total Price can be created from the ordered products.
                                //Total Price is now being stored in the database.
                                _product = _storeBL.GetAllProducts().Find(p => p.productID == items.productID);
                                priceTotal += _product.Price * _product.Quantity;
                            }
                            //Total price expressed with two decimal places.
                            priceTotal = Math.Round(priceTotal, 2);
                            Console.WriteLine("");
                            Console.WriteLine("Total Price: $" + priceTotal);

                            //One last menu to finalize the order, or cancel it.
                            Console.WriteLine("Please choose if you wish to submit the order or cancel it.");
                            Console.WriteLine("[1] - Submit the order");
                            Console.WriteLine("[2] - Cancel the order");
                            string submitChoice = Console.ReadLine();
                            if (submitChoice == "1")
                            {
                                Log.Information("User selected to submit an order.");
                                //Adds new order to the database using the StoreFront BL.
                                //Inventory updated with subtracted quantity of products in SQL Repository.
                                _storeBL.PlaceNewOrder(_customerID, _storeID, priceTotal, orderedItems);
                                
                                Console.WriteLine("Thank you for your order!");
                                Console.WriteLine("Please press the Enter key to continue:");
                                Console.ReadLine();
                                Log.Information("User pressed the Enter key to continue.");
                            }
                            else if (submitChoice == "2")
                            {
                                Log.Information("User selected to cancel an order.");

                                //Last chance to cancel the order.
                                Console.WriteLine("Cancelling order. Press the enter key to return to the Order Menu:");
                                Console.ReadLine();
                                Log.Information("User pressed the Enter key to return to the PlaceOrder menu.");
                                shoploop = false;
                                return "PlaceOrder";
                            }
                            else
                            {
                                Log.Warning("User selected an invalid response.");
                                Console.WriteLine("You've selected an invalid response.");
                                Console.WriteLine("Press the Enter key to try again:");
                                Console.ReadLine();
                                Log.Information("User pressed the Enter key to try again.");
                            }
                        }
                        else if (orderchoice == "3")
                        {
                            Log.Information("User selected to cancel an order.");
                            
                            //First chance to cancel the order
                            Console.WriteLine("Cancelling order. Press the enter key to return to the Order Menu:");
                            Console.ReadLine();
                            Log.Information("User pressed the Enter key to try again.");
                            shoploop = false;
                            return "PlaceOrder";
                        }
                        else
                        {
                            Log.Warning("User selected an invalid response.");
                            Console.WriteLine("You've selected an invalid response.");
                            Console.WriteLine("Press the Enter key to try again:");
                            Console.ReadLine();
                            Log.Information("User pressed the Enter key return to the PlaceOrder menu.");
                        }
                    }
                    return "PlaceOrder";
                case "2":
                    Log.Information("User has selected to return to the main menu.");
                    return "MainMenu";
                default:
                    Log.Warning("User selected an invalid response.");
                    Console.WriteLine("You've selected an invalid reponse.");
                    Console.WriteLine("Please press the Enter key to try again.");
                    Console.ReadLine();
                    Log.Information("User pressed the Enter key to try again.");
                    return "PlaceOrder";
            }
        }
    }
}