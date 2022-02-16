using ShopBL;
using ShopModel;


namespace ShopUI
{
    public class SearchCustomerMenu : IMenu
    {

        //Dependency Injection ======
        private ICustomerBL _customerBL;
        public SearchCustomerMenu(ICustomerBL p_customerBL)
        {
            _customerBL = p_customerBL;
        }
        // ===========================
        
        public void Display()
        {
            Console.WriteLine("Please enter the type of Customer information you would like to search for:\n");
            Console.WriteLine("[1] - Customer Name");
            Console.WriteLine("[2] - Customer Email");
            Console.WriteLine("[3] - Customer Phone Number");
            Console.WriteLine("[4] - Return to Main Menu\n");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Log.Information("User selected to enter customer name.");
                    Console.WriteLine("Please enter a name:"); //Logic to grab user input
                    string name = Console.ReadLine();
                    Log.Information("User has entered a name.");
                    try
                    {
                        List<Customer> listofCustomer = _customerBL.SearchCustomer("1", name); //logic to display result
                        foreach (var item in listofCustomer)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine("-------------------------");
                        }
                        Log.Information("Successfully retrieved and displayed customer information.");
                        Console.WriteLine("");
                        Console.WriteLine("Press the Enter key to continue.");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue:");
                    }
                    catch (System.Exception)
                    {
                        Log.Warning("User's inputed name could not be found.");
                        Console.WriteLine("Customer name could not be found. Make sure you are spelling correctly.");
                        Console.WriteLine("Please press the Enter key to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                    }
                    return "SearchCustomerMenu";
                case "2":
                    Log.Information("User selected to enter customer email.");
                    Console.WriteLine("Please enter an email:"); 
                    string email = Console.ReadLine();
                    Log.Information("User entered in an email.");
                    try
                    {
                        List<Customer> listofCustomer = _customerBL.SearchCustomer("2", email);
                        foreach (var item in listofCustomer)
                        {
                            Console.WriteLine("-------------------------");
                            Console.WriteLine(item);
                            Console.WriteLine("-------------------------");
                        }
                        Log.Information("Successfully retrieved and displayed customer information.");
                        Console.WriteLine("");
                        Console.WriteLine("Press the Enter key to continue:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue.");
                    }
                    catch (System.Exception)
                    {
                        Log.Warning("User's inputed email could not be found.");
                        Console.WriteLine("Customer email could not be found. Make sure you are spelling correctly.");
                        Console.WriteLine("Please press the Enter key to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                    }
                    return "SearchCustomerMenu";
                case "3":
                    Log.Information("User selected to enter customer phone number.");
                    Console.WriteLine("Please enter phone number (must be 10 digits with no dashes):"); 
                    string phone = Console.ReadLine();
                    Log.Information("User entered in a phone number.");
                    try
                    {
                        List<Customer> listofCustomer = _customerBL.SearchCustomer("3", phone);
                        foreach (var item in listofCustomer)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine("-------------------------");
                        }
                        Log.Information("Successfully retrieved and displayed customer information.");
                        Console.WriteLine("");
                        Console.WriteLine("Press the Enter key to continue:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue.");
                    }
                    catch (System.Exception)
                    {
                        Log.Warning("User's inputed phone number could not be found.");
                        Console.WriteLine("Customer phone number could not be found. Make sure you are spelling correctly.");
                        Console.WriteLine("Please press the Enter key to try again:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to try again.");
                    }
                    return "SearchCustomerMenu";
                case "4":
                    Log.Information("User has selected to return to the main menu.");
                    return "MainMenu";
                default:
                    Log.Warning("User selected an invalid response.");
                    Console.WriteLine("You've selected an invalid reponse.");
                    Console.WriteLine("Please press the Enter key to try again:");
                    Console.ReadLine();
                    Log.Information("User has pressed the Enter Key to try again.");
                    return "SearchCustomerMenu";
            }
        }
    }
}
            
