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
        private IOrderBL _orderBL;
        public PlaceOrder(ICustomerBL p_customerBL, IStoreFrontBL p_storeBL, IOrderBL p_orderBL)
        {
            _customerBL = p_customerBL;
            _storeBL = p_storeBL;
            _orderBL = p_orderBL;
            _listofCustomer = _customerBL.GetAllCustomer();
            _listofStoreFront = _storeBL.GetAllStoreFront();
        }
        private static List<LineItem> orderedItems = new List<LineItem>();
        public static int _customerID;
        public static int _storeID;
        public static double _priceTotal;
        public static int _prodID;
        public static int _qty;


        public void Display()
        {
            //The menu that allows user to place on order.
            Console.WriteLine("Welcome to the Colorado's Market Order Menu. To begin your order, you must enter your customer email from the list below");
            Console.WriteLine("as well as the ID of the store you want to purchase from.\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - Place an order");
            Console.WriteLine("[2] - Return to the Main Menu\n");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    //Displays customers in the database.
                    Log.Information("User selected to place an order.");
                    Console.WriteLine("First, please enter an email:"); 
                    string email = Console.ReadLine();
                    Log.Information("User entered in an email.");

                    try
                    {
                        List<Customer> listofCustomer = _customerBL.SearchCustomer("2", email);
                        foreach (var item in listofCustomer)
                        {
                            _customerID = item.customerID;
                            Console.WriteLine("-------------------------");
                            Console.WriteLine(item);
                            Console.WriteLine("-------------------------");
                        }
                        Log.Information("Successfully retrieved and displayed customer information.");
                        Console.WriteLine("");
                        Console.WriteLine("Your customer information is displayed above. Press the Enter key to continue to the Store List:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue:");
                    }
                    // Console.WriteLine("=============== Customer List ===============");
                    // foreach (var item in _listofCustomer)
                    // {
                    //     Console.WriteLine(item.customerID);
                    //     Console.WriteLine(item.Name);
                    //     Console.WriteLine("-------------------------");
                    // }
                    // Console.WriteLine("");
                    // Console.WriteLine("Please enter a customer ID:");
                    // try
                    // {
                    //     //Get the customer ID from the user.
                    //     _customerID = Convert.ToInt32(Console.ReadLine());
                    //     Log.Information("User has entered a customer ID.");
                    //     if ((_listofCustomer.All(p => p.customerID != _customerID)))
                    //     {
                    //         throw new Exception("Customer ID cannot be found.");
                    //     }
                    // }
                    catch (System.Exception)
                    {
                        Log.Warning("Customer email could not be found in database.");
                        Console.WriteLine("Customer email could not be found. Make sure you are spelling correctly.");
                        Console.WriteLine("Please press the Enter key to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                        return "PlaceOrder";
                    }
                    //Displays the store list from the database.
                    Console.WriteLine("=============== Store List ===============");
                    foreach (var item in _listofStoreFront)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine("-------------------------");
                    }
                    Console.WriteLine("");
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
                            Console.WriteLine(product);    
                            Console.WriteLine("-------------------------");                    
                        }
                        Log.Information("Successfully retrieved and displayed current inventory in a store.");
                        Console.WriteLine("");
                    }
                    catch (System.Exception)
                    {
                        Log.Warning("Products could not be found in store.");
                        Console.WriteLine("There are currently no products in this store.");
                        Console.WriteLine("Press the Enter button to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                        return "Place Order"; 
                    }
                    bool shoploop = true;
                    while (shoploop)
                    {
                        //The order menu is stored in a while-loop to allow users to add products to their order repeatedly before checking out.
                        Console.WriteLine("To add product to your order, you must enter the product's ID from the inventory list.");
                        Console.WriteLine("What would you like to do next?\n");
                        Console.WriteLine("[1] - Add a product to an order");
                        Console.WriteLine("[2] - Check out");
                        Console.WriteLine("[3] - Cancel my order\n");
                        string orderchoice = Console.ReadLine();

                        if (orderchoice == "1")
                        {
                            Log.Information("User selected to add product to an order.");

                            //Products' information  (ID and quantity) are specified by the user to add to the order.
                            try
                            {
                                Console.WriteLine("Please enter the product ID:");
                                _prodID = Convert.ToInt32(Console.ReadLine());
                                Log.Information("User has entered a product ID.");

                                Console.WriteLine("Now, enter the amount of the product you wish to order:");
                                _qty = Convert.ToInt32(Console.ReadLine());
                                Log.Information("User has entered a product qty.");

                                orderedItems.Add(new LineItem()
                                {
                                    productID = _prodID,
                                    Quantity = _qty
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
                                int orderItemQty = _qty;
                                List<Product> listofProducts = _storeBL.GetProductbyStoreID(_storeID);
                                foreach (var item in listofProducts)
                                {
                                    inventory = item.Quantity;
                                }

                                if (orderItemQty <= inventory)
                                {
                                    Console.WriteLine("\n\n");
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
                                _priceTotal = 0;
                                Log.Warning("User entered more products than what's currently stored in the inventory.");
                                Console.WriteLine("You cannot order more products than the inventory holds!");
                                Console.WriteLine("Press the the Enter key to try again:");
                                Console.ReadLine();
                                Log.Information("User pressed the Enter key to try again.");
                                shoploop = false;
                                return "PlaceOrder";
                            }
                        }
                        else if (orderchoice == "2")
                        {
                            Log.Information("User selected to check out an order.");

                            //Break the order menu loop to finally check out with the products.
                            shoploop = false;
                            _priceTotal = 0;
                            Console.WriteLine("Order has been checked out!");

                            Product _product = new Product();
                            foreach (var items in orderedItems)
                            {
                                //Create a list storing order items into an updated list.
                                //Using the variable stored above, Total Price can be created from the ordered products.
                                //Total Price is now being stored in the database.
                                _product = _storeBL.GetProductbyStoreID(_storeID).Find(p => p.productID == items.productID);
                                _priceTotal += _product.Price * items.Quantity;
                            }
                            //Total price expressed with two decimal places.
                            _priceTotal = Math.Round(_priceTotal, 2);
                            Console.WriteLine("Total Price: $" + _priceTotal);

                            //One last menu to finalize the order, or cancel it.
                            Console.WriteLine("");
                            Console.WriteLine("Please choose if you wish to submit the order or cancel it.\n");
                            Console.WriteLine("[1] - Submit the order");
                            Console.WriteLine("[2] - Cancel the order\n");
                            string submitChoice = Console.ReadLine();
                            if (submitChoice == "1")
                            {
                                Log.Information("User selected to submit an order.");
                                //Adds new order to the database using the StoreFront BL.
                                //Inventory updated with subtracted quantity of products in SQL Repository.
                                _orderBL.PlaceNewOrder(_customerID, _storeID, _priceTotal, orderedItems);
                                
                                Console.WriteLine("Thank you for your order!");
                                Console.WriteLine("Please press the Enter key to continue:");
                                Console.ReadLine();
                                Log.Information("User pressed the Enter key to continue.");
                            }
                            else if (submitChoice == "2")
                            {
                                Log.Information("User selected to cancel an order.");

                                //Last chance to cancel the order.
                                Console.WriteLine("Order has been cancelled. Press the enter key to return to the Order Menu:");
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
                            Console.WriteLine("Order has been cancelled. Press the enter key to return to the Order Menu:");
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