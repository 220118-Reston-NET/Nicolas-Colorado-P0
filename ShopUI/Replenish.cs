using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class Replenish
    {
        private List<StoreFront> _listofStoreFront;

        //Dependency Injection with StoreFrontBL
        private IStoreFrontBL _storeBL;

        public Replenish(IStoreFrontBL p_storeBL)
        {
            _storeBL = p_storeBL;
            _listofStoreFront = _storeBL.GetAllStoreFront();
        }

        public void Display()
        {
            foreach (var item in _listofStoreFront)
            {
                Console.WriteLine("====================");
                Console.WriteLine(item);
            }
            
            Console.WriteLine("Displayed above is a list of stores currently available.\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - View a store's inventory");
            Console.WriteLine("[2] - Return to Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Please enter store's ID:");
                    try
                    {
                        int storeID = Convert.ToInt32(Console.ReadLine());
                        List<Product> listofProducts = _storeBL.GetProductbyStoreID(storeID);
                        Console.WriteLine("=============== Inventory ===============");
                        foreach (var item in listofProducts)
                        {
                            Console.WriteLine("====================");
                            Console.WriteLine(item);
                        }

                        Console.WriteLine("Please enter the ID of the product you wish to replenish:");
                        int inventoryID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the quantity of items you are adding:");
                        int itemQuantity = Convert.ToInt32(Console.ReadLine());
                        _storeBL.ReplenishInventory(inventoryID, itemQuantity);

                        Console.WriteLine("Product has been successfully replenished. Inventory has been updated");
                        Console.WriteLine("Please press the Enter key to continue.");
                        Console.ReadLine();
                        return "MainMenu"; 
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("You've selected an invalid response.");
                        Console.WriteLine("Press the Enter button to try again.");
                        return "Replenish";  
                    }
                    return "Replenish";
                case "2":
                    return "MainMenu";
                default:
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to try again.");
                    return "Replenish";
            }
        }
    }
}