using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class ViewStoreOrder : IMenu
    {
        private List<StoreFront> _listofStoreFront;

        //Dependency Injection with StoreFrontBL. Same as with ViewCustomerOrder
        private IStoreFrontBL _storeBL;
        public ViewStoreOrder(IStoreFrontBL p_storeBL)
        {
            _storeBL = p_storeBL;
            _listofStoreFront = _storeBL.GetAllStoreFront();
        }

        public void Display()
        {
            //The menu that shows a list of stores that a user can choose from.
            Console.WriteLine("Displayed below is a list of stores currently in our database. To view a store's order history, you must enter its ID.\n");
            Console.WriteLine("=============== Store List ===============");
            foreach (var item in _listofStoreFront)
            {
                Console.WriteLine(item);
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine("");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - Select a store by ID");
            Console.WriteLine("[2] - Return to Main Menu\n");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Log.Information("User selected to view a store's order history.");
                    Console.WriteLine("Please enter store's ID:");
                    try
                    {
                        //Gets the storeID from the user.
                        int sfID = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User has entered a store ID.");
                        if ((_listofStoreFront.All(p => p.storeID != sfID)))
                        {
                            throw new Exception("Store ID cannot be found.");
                        }

                        //Displays the order history from the store.
                        List<Orders> listofOrders = _storeBL.GetOrderbyStoreID(sfID);
                        Console.WriteLine("=============== Order History ===============");
                        foreach (var item in listofOrders)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine("-------------------------");
                        }
                        Log.Information("Successfully retrieved and displayed list of orders from store ID.");
                        Console.WriteLine("");
                        Console.WriteLine("Please press the Enter key to continue:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue.");
                    }
                    catch (FormatException)
                    {
                        //Displays if user input has an invalid format.
                        Log.Warning("User inputted a invalid value for store ID.");
                        Console.WriteLine("You've selected an invalid value.");
                        Console.WriteLine("Press the Enter button to continue:");
                        Console.ReadLine();
                        Log.Information("User has pressed the Enter key to try again.");
                    }
                    return "ViewStoreOrder";
                case "2":
                    Log.Information("User has selected to return to the main menu.");
                    return "MainMenu";
                default:
                    Log.Warning("User selected an invalid response.");
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to continue:");
                    Log.Information("User has pressed the Enter key to try again.");
                    return "ViewStoreOrder";
            }
        }
    }
}
