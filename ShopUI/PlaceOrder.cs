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
        private static LineItem _newLineItem = new LineItem();
        private static List<Product> listOfProducts = new List<Product>();
        public static int _customerID;
        public static int _storeID;


        public void Display()
        {
            Console.WriteLine("Welcome to the Colorado's Market Order Menu. To begin your order, you must enter your customer ID on file\n");
            Console.WriteLine("as well as the ID of the store you want to purchase from.");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - Purchase items");
            Console.WriteLine("[2] - Return to the Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Please enter a customer ID:");
                    try
                    {
                        //Get the customer ID
                        _customerID = Convert.ToInt32(Console.ReadLine());
                        if ((_listofCustomer.All(p => p.customerID != _customerID)))
                        {
                            throw new Exception("Customer ID cannot be found.");
                        }
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Please press the Enter key to try again.");
                        Console.ReadLine();
                        return "PlaceOrder";
                    }
                    Console.WriteLine("Please enter the store's ID:");
                    try
                    {
                        //Get the store ID
                        _storeID = Convert.ToInt32(Console.ReadLine());
                        if ((_listofStoreFront.All(p => p.storeID != _storeID)))
                        {
                            throw new Exception("Store ID could not be found.");
                        }
                        List<Product> listofProducts = _storeBL.GetProductbyStoreID(_storeID);
                        Console.WriteLine("Please check out our product menu below:\n");
                        foreach (var product in listofProducts)
                        {
                            
                            Console.WriteLine("--------------------");
                            Console.WriteLine(product);                        
                        }
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Press the Enter button to try again.");
                        Console.ReadLine();
                        return "Place Order"; 
                    }
                    bool shoploop = true;
                    while (shoploop)
                    {
                        Console.WriteLine("To add product to your order, you must enter the product's ID. What would you like to do next?");
                        Console.WriteLine("[1] - Add a product to an order");
                        Console.WriteLine("[2] - Check out");
                        Console.WriteLine("[3] - Cancel my order");
                        string orderchoice = Console.ReadLine();

                        if ( orderchoice == "1")
                        {
                            //Products info converted into new Line items
                            Console.WriteLine("Please enter product ID:");
                            int prodID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Now, enter the amount of the product you wish to order:");
                            int qty = Convert.ToInt32(Console.ReadLine());

                            orderedItems.Add(new LineItem()
                            {
                                productID = prodID,
                                Quantity = qty
                            });

                            //Makes sure the items ordered don't exceed the quantity of products in the inventory.
                            //inventory object stores quantity of each product
                            try
                            {
                                int inventory = 0;
                                int orderItemQty = qty;
                                List<Product> listofProducts = _storeBL.GetAllProducts();
                                foreach (var item in listofProducts)
                                {
                                    inventory = item.Quantity;
                                }

                                if (orderItemQty <= inventory)
                                {
                                    Console.WriteLine("Product(s) has been added to your order!\n");
                                }
                            }
                            catch (System.Exception)
                            {
                                throw new Exception("You cannot order more products than the inventory holds!");
                                Console.WriteLine("Press the the Enter key to try again");
                                Console.ReadLine();
                                shoploop = false;
                                return "PlaceOrder";
                            }
                        }
                        else if (orderchoice == "2")
                        {
                            shoploop = false;
                            double priceTotal = 0;
                            Console.WriteLine("Order is currently being checked out!\n");

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

                            Console.WriteLine("Please choose if you wish to submit the order or cancel it.");
                            Console.WriteLine("[1] - Submit the order");
                            Console.WriteLine("[2] - Cancel the order");
                            string submitChoice = Console.ReadLine();
                            if (submitChoice == "1")
                            {

                                //Adds new order to the database using the StoreFront BL.
                                //This step is similar to the AddCustomer method
                                //Inventory updated with subtracted quantity of products in SQL Repository
                                _storeBL.PlaceNewOrder(_customerID, _storeID, priceTotal, orderedItems);
                                
                                Console.WriteLine("Thank you for your order!");
                                Console.WriteLine("Please press the Enter key to return to the Order Menu.");
                                Console.ReadLine();
                                return "PlaceOrder";
                            }
                            else if (submitChoice == "2")
                            {
                                //Last chance to cancel the order
                                Console.WriteLine("Cancelling order. Press the enter key to return to the Order Menu.");
                                Console.ReadLine();
                                shoploop = false;
                                return "PlaceOrder";
                            }
                            else
                            {
                                Console.WriteLine("You've made an invalid selection.");
                                Console.WriteLine("Press the Enter key to try again:");
                                Console.ReadLine();
                            }
                        }
                        else if (orderchoice == "3")
                        {
                            //First chance to cancel the order
                            Console.WriteLine("Cancelling order. Press the enter key to return to the Order Menu.");
                            Console.ReadLine();
                            shoploop = false;
                            return "PlaceOrder";
                        }
                        else
                        {
                            Console.WriteLine("You've made an invalid selection.");
                            Console.WriteLine("Press the Enter key to try again:");
                            Console.ReadLine();
                        }
                    }
                    return "PlaceOrder";
                case "2":
                    return "MainMenu";
                default:
                    Console.WriteLine("You've selected an invalid reponse.");
                    Console.WriteLine("Please press the Enter key to try again.");
                    Console.ReadLine();
                    return "PlaceOrder";
            }
        }
    }
}