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
            foreach (var item in _listofStoreFront)
            {
                Console.WriteLine("====================");
                Console.WriteLine(item);
            }
            
            Console.WriteLine("Displayed above is a list of stores currently in our database. To view a store's order history, enter its ID.");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - Select a store by ID");
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
                        List<Orders> listofOrders = _storeBL.GetOrderbyStoreID(storeID);
                        foreach (var item in listofOrders)
                        {
                            Console.WriteLine("====================");
                            Console.WriteLine(item);
                        }

                        Console.WriteLine("Please press the Enter button to continue");
                        Console.ReadLine();
                    
                        return "MainMenu"; 
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("You've selected an invalid response.");
                        Console.WriteLine("Press the Enter button to continue.");
                        return "ViewStoreOrder";  
                    }
                    return "ViewStoreOrder";
                case "2":
                    return "MainMenu";
                default:
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to continue.");
                    return "ViewStoreOrder";
            }
        }
    }
}
