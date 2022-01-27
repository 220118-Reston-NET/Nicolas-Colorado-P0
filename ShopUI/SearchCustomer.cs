using ShopBL;
using ShopModel;


namespace ShopUI
{
    public class SearchCustomer : IMenu
    {
        //static non-access modifier is needed to keep this variable consistent
        //to all objects we create
        private static Customer _newCustomer = new Customer();

        //Dependency Injection ======
        private ICustomerBL _customerBL;
        public AddCustomerMenu(ICustomerBL p_customerBL)
        {
            _customerBL = p_customerBL;
        }
        // ===========================
        
        public void Display()
        {
            Console.WriteLine("Please enter the type of Customer information you would like to search for:");
            Console.WriteLine("[1] - Customer Name");
            Console.WriteLine("[2] - Customer Address");
            Cosnole.WriteLine("[3] - Cusutomer Email");
            Console.WriteLine("[4] - Customer Phone Number");
            Console.WriteLine("[5] - Return to Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Please enter a name:"); //Logic to grab user input
                    string name = Console.ReadLine();
                    List<Customer> listofCustomer = _customerBL.SearchCustomer(name); //logic to display result
                    foreach (var item in listofCustomer)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press the Enter button to continue");
                    Console.ReadLine();
                    return "MainMenu";
                case "2":
                    Console.WriteLine("Please enter an address:"); 
                    string address = Console.ReadLine();
                    List<Customer> listofCustomer = _customerBL.SearchCustomer(address);
                    foreach (var item in listofCustomer)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press the Enter button to continue");
                    Console.ReadLine();
                    return "MainMenu";
                case "3":
                    Console.WriteLine("Please enter an email:"); 
                    string email = Console.ReadLine();
                    List<Customer> listofCustomer = _customerBL.SearchCustomer(email);
                    foreach (var item in listofCustomer)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press the Enter button to continue");
                    Console.ReadLine();
                    return "MainMenu";
                case "4":
                    Console.WriteLine("Please enter phone number (must be 10 digits):"); 
                    double phone = Console.ReadLine();
                    List<Customer> listofCustomer = _customerBL.SearchCustomer(phone);
                    foreach (var item in listofCustomer)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press the Enter button to continue");
                    Console.ReadLine();
                    return "MainMenu";
                case "5":
                    return "MainMenu";
                default:
                    Console.WriteLine("You've entered an invalid reponse.");
                    Console.WriteLine("Please press the Enter button  to continue");
                    Console.ReadLine();
                    return "SearchCustomer";
            }
        }
    }
}
            
