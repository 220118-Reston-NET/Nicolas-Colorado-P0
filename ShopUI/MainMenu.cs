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
            Console.WriteLine("Welcome to Nick's Pawn Shop!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] - Add customer information");
            Console.WriteLine("[2] - Search for a customer");
            Console.WriteLine("[3] - Place an order");
            Console.WriteLine("[4] - View order history");
            Console.WriteLine("[5] - View storefront inventory");
            Console.WriteLine("[6] - Replenish storefront inventory");
            Console.WriteLine("[7] - Exit");

        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();
            
            //Switch cases are useful if you are doing a bunch of comparisons

            switch (userInput)
            {
                case "1":
                    return "AddCustomerMenu";
                case "2":
                    return "SearchCustomer";
                case "3":
                    return "PlaceOrder"; 
                case "4":
                    return "ViewOrderHistory";
                case "5":
                    return "ViewStoreFront";
                case "6":
                    return "Replenish";
                case "7":
                    return "Thank you for visiting Nick's Pawn Shop! Have a splendid day!";
                default:
                    Console.WriteLine("Please input a valid response.");
                    Console.WriteLine("Press the Enter button to continue.");
                    return MainMenu;

            }
        }
    }
}