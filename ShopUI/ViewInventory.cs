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

                        Console.WriteLine("Please press the Enter button to continue");
                        Console.ReadLine();
                    
                        return "MainMenu"; 
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Store information could not be found.");
                        Console.WriteLine("Press the Enter button to try again.");
                        Console.ReadLine();
                        return "ViewInventory";  
                    }
                    return "ViewInventory";
                case "2":
                    return "MainMenu";
                default:
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to continue.");
                    return "ViewInventory";
            }
        }
    }
}