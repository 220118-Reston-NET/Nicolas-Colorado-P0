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
            Console.WriteLine("[6] - Return to Main Menu");

        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Please enter a name:");
                    string choice = Console.ReadLine();
                    if (choice.IsAlphabetic() == false)
                    {
                        Console.WriteLine("Name must only contain alpabetic characters (abcABC).");
                        return userInput == "1";
                    }
                    else
                    {
                        choice = _newCustomer.Name;
                        return "AddCustomerMenu";
                    }
                    
                case "2":
                    Console.WriteLine("Please enter an address:");
                    _newCustomer.Address = Console.ReadLine();
                    return "AddCustomerMenu";
                case "3":
                    Console.WriteLine("Please enter an email:");
                    _newCustomer.Email = Console.ReadLine();
                    return "AddCustomerMenu";
                case "4":
                    Console.WriteLine("Please enter a phone number:");
                    _newCustomer.Phone = Console.ReadLine();
                    return "AddCustomerMenu";
                case "5":
                    //Exception handling with logging to have better user experience
                    try
                    {
                        Log.Information("Adding customer information \n" + _newCustomer);
                        _customerBL.AddCustomerMenu(_newCustomer);
                        Log.Information("Successfully added customer's information!");
                    }
                    catch (System.Exception exc)
                    {
                        Log.Warning("Failed to add customer information");
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                    }

                    return "MainMenu";
                    
                case "6":
                    Console.WriteLine("Are you sure you want to exit?");
                    Console.WriteLine("Press the 1 key to return to the Main Menu. Otherwise, press any key to stay in the current menu.");
                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        return "MainMenu";
                    }
                    else 
                    {
                        return "AddCustomerMenu";
                    }
                default:                  
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to continue.");
                    Console.ReadLine();
                    return "AddCustomerMenu";
            }
        }
    }   

}