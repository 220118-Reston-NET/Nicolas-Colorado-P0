namespace ShopUI
{
    /*
        MainMenu inherits IMenu interface but since it is a class 
        it needs to give actual implementation details to the methods
        stated inside of the interface
    */
    public class MainMenu : IMenu
    {
        public void Display()
        {
            Console.WriteLine("Welcome to Nick's Pawn Shop!\n");
            Console.WriteLine("Please select from the list of options below:\n");
            Console.WriteLine("========== Customer Menu ==========");
            Console.WriteLine("[1] - Enroll as a new customer");
            Console.WriteLine("[2] - Place an order");
            Console.WriteLine("[3] - View a customer's order history\n");
            Console.WriteLine("========== Employee Menu ==========");
            Console.WriteLine("[4] - Search for a customer");
            Console.WriteLine("[5] - View a store's order history");
            Console.WriteLine("[6] - Replenish storefront inventory\n");
            Console.WriteLine("========== Other Options ==========");
            Console.WriteLine("[7] - View a store's inventory");
            Console.WriteLine("[8] - Exit\n");

        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();
            
            //Switch cases are useful if you are doing a bunch of comparisons

            switch (userInput)
            {
                case "1":
                    return "AddCustomerMenu";
                case "4":
                    return "SearchCustomerMenu";
                // case "2":
                //     return "PlaceOrder"; 
                case "3":
                    return "ViewCustomerOrder";
                case "5":
                    return "ViewStoreOrder";
                case "7":
                    return "ViewInventory";
                // case "6":
                //     return "Replenish";
                case "8":
                    Console.WriteLine("Thank you for visiting Nick's Pawn Shop! Have a splendid day!");
                    return "Exit";
                default:
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter key to continue.\n");
                    Console.ReadLine();
                    return "MainMenu";

            }
        }
    }
}