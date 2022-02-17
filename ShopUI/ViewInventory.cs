using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class ViewInventory : IMenu
    {
        private List<StoreFront> _listofStoreFront;

        //Dependency Injection with StoreFrontBL
        private IStoreFrontBL _storeBL;
        public ViewInventory(IStoreFrontBL p_storeBL)
        {
            _storeBL = p_storeBL;
            _listofStoreFront = _storeBL.GetAllStoreFront();
        }

        public void Display()
        {
            //The menu that displays a list of stores in the database that the user can choose from to get the inventory.
            Console.WriteLine("Displayed below is a list of stores currently available for online orders. To view a store's inventory, enter its ID.\n");
             Console.WriteLine("=============== Store List ===============");
            foreach (var item in _listofStoreFront)
            {
                Console.WriteLine(item);
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine("");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - View a store's inventory");
            Console.WriteLine("[2] - Return to Main Menu\n");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Log.Information("User selected to view a store's inventory.");
                    Console.WriteLine("Please enter store's ID:");
                    try
                    {
                        //Gets the store ID from the user.
                        int sfID = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User has entered a store ID.");
                        if ((_listofStoreFront.All(p => p.storeID != sfID)))
                        {
                            throw new Exception("Store ID cannot be found.");
                        }

                        //Gets and displays the inventory based on store ID.
                        List<Product> listofProducts = _storeBL.GetProductbyStoreID(sfID);
                        Console.WriteLine("=============== Inventory ===============");
                        foreach (var item in listofProducts)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine("-------------------------");
                        }
                        Log.Information("Successfully retrieved and displayed current inventory in a store.");
                        Console.WriteLine("");
                        Console.WriteLine("Please press the Enter key to continue");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue.");
                    }
                    catch (System.Exception)
                    {
                        //Displays if stored ID is invalid, or doesn't exist.
                        Log.Warning("User inputted a invalid value for store ID.");
                        Console.WriteLine("Store information could not be found.");
                        Console.WriteLine("Press the Enter button to try again.");
                        Console.ReadLine();
                        Log.Information("User has pressed the Enter key to try again."); 
                    }
                    return "ViewInventory";
                case "2":
                    Log.Information("User has selected to return to the main menu.");
                    return "MainMenu";
                default:
                    Log.Warning("User selected an invalid response.");
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to try again.");
                    Console.ReadLine();
                    Log.Information("User has pressed the Enter key to try again.");
                    return "ViewInventory";
            }
        }
    }
}