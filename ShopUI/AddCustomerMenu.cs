using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class AddCustomerMenu : IMenu
    {
        //static non-access modifier is needed to keep this variable consistent
        //to all objects we create
        private static Customer _newCustomer = new Customer();

        //Dependency Injection =======
        private ICustomerBL _customerBL;
        public AddCustomerMenu(ICustomerBL p_customerBL)
        {
            _customerBL = p_customerBL;
        }
        // ===========================
        
        public void Display() 
        {
            Console.WriteLine("Please select the type of Customer information you would like to add:\n");
            Console.WriteLine("[1] - Name: " + _newCustomer.Name); 
            Console.WriteLine("[2] - Address: " + _newCustomer.Address); 
            Console.WriteLine("[3] - Email: " + _newCustomer.Email); 
            Console.WriteLine("[4] - Phone: " + _newCustomer.Phone);  
            Console.WriteLine("[5] - Save Information");
            Console.WriteLine("[6] - Return to Main Menu\n");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Log.Information("User selected to add customer name.");
                    Console.WriteLine("Please enter a name:");
                    _newCustomer.Name = Console.ReadLine(); 
                    Log.Information("User has entered in a name.");
                    return "AddCustomerMenu";
                case "2":
                    Log.Information("User selected to add customer address.");
                    Console.WriteLine("Please enter an address:");
                    _newCustomer.Address = Console.ReadLine();
                    Log.Information("User has entered in an address.");
                    return "AddCustomerMenu";
                case "3":
                    Log.Information("User selected to add customer email.");
                    Console.WriteLine("Please enter an email:");
                    _newCustomer.Email = Console.ReadLine();
                    Log.Information("User has entered in an email.");
                    return "AddCustomerMenu";
                case "4":
                    Log.Information("User selected to add customer phone number.");
                    Console.WriteLine("Please enter a phone number (must be 10 digits with no dashes):");
                    _newCustomer.Phone = Console.ReadLine();
                    Log.Information("User has entered in a phone number.");
                    return "AddCustomerMenu";
                case "5":
                    //Exception handling with logging to have better user experience.
                    try
                    {
                        Log.Information("Adding customer information \n" + _newCustomer);
                        _customerBL.AddCustomer(_newCustomer);
                        Console.WriteLine("Customer information has been added. Welcome to Colorado's Market!");
                        Console.WriteLine("Press the Enter to return to the main menu:");
                        Console.ReadLine();
                        Log.Information("Successfully added customer's information!");
                    }
                    catch (System.Exception exc)
                    {
                        Log.Warning("Failed to add customer information.");
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue.");
                    }
                    return "MainMenu";
                case "6":
                    Log.Information("User has selected to return to the main menu.");
                    return "MainMenu";
                default:      
                    Log.Warning("User selected an invalid response.");            
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter key to try again:");
                    Console.ReadLine();
                    Log.Information("User has pressed the Enter Key to try again.");
                    return "AddCustomerMenu";
            }
        }
    }   
}