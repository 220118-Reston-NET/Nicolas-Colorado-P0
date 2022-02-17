using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class Replenish : IMenu
    {
        private List<StoreFront> _listofStoreFront;

        //Dependency Injection with StoreFrontBL
        private IStoreFrontBL _storeBL;
        public Replenish(IStoreFrontBL p_storeBL)
        {
            _storeBL = p_storeBL;
            _listofStoreFront = _storeBL.GetAllStoreFront();
        }

        public int _sfID;

        public void Display()
        {
            Console.WriteLine("Displayed below is a list of stores currently available for online orders. To replenish a product, select a store's inventory.\n");
            Console.WriteLine("=============== Store List ===============");
            foreach (var item in _listofStoreFront)
            {
                Console.WriteLine(item);
                Console.WriteLine("-------------------------");
            }
            
            //The menu that offers a choice to replenish inventory or not.
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - Replenish store's inventory");
            Console.WriteLine("[2] - Return to Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Log.Information("User selected to replenish a store's inventory.");
                    Console.WriteLine("Please enter store's ID:");
                    try
                    {
                        //Get storeID from the user.
                        _sfID = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User has entered a store ID.");
                        List<Product> listofProducts = _storeBL.GetProductbyStoreID(_sfID);
                        Console.WriteLine("=============== Inventory ===============");
                        foreach (var item in listofProducts)
                        {
                            Console.WriteLine("-------------------------");
                            Console.WriteLine(item);
                        }
                        Log.Information("Successfully retrieved and displayed current inventory in a store.");
                        Console.WriteLine("");
                    }
                    catch (FormatException)
                    {
                        Log.Warning("User inputted a invalid value for productID.");
                        Console.WriteLine("You've selected an invalid value.");
                        Console.WriteLine("Press the Enter button to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                        return "Replenish";
                    }
                    try
                    {
                        //Gets the productID and Quantity from the user to add to the inventory.
                        Console.WriteLine("Please enter the ID of the product you wish to replenish:");
                        int inventoryID = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User has entered a product ID.");

                        Console.WriteLine("Enter the quantity of items you are adding:");
                        int itemQuantity = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User has entered a product quantity.");

                        //Repenishes the inventory.
                        _storeBL.ReplenishInventory(inventoryID, itemQuantity, _sfID);

                        Console.WriteLine("Product has been successfully replenished. Inventory has been updated");
                        Console.WriteLine("Please press the Enter key to continue:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue.");
                    }
                    catch (FormatException)
                    {
                        //Either inventoryID or item quantity were given invalid data.
                        Log.Warning("User inputted an invalid value for inventoryID or itemQuantity.");
                        Console.WriteLine("You've selected invalid values for inventoryID or itemQuantity.");
                        Console.WriteLine("Press the Enter button to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                    }
                    return "Replenish";
                case "2":
                    Log.Information("User has selected to return to the main menu.");
                    return "MainMenu";
                default:
                    Log.Warning("User selected an invalid response.");
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to try again:");
                    Console.ReadLine();
                    Log.Information("User pressed the Enter key to try again.");
                    return "Replenish";
            }
        }
    }
}