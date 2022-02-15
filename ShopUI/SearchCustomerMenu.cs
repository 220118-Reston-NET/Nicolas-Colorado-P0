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
            Console.WriteLine("[2] - Cusutomer Email");
            Console.WriteLine("[3] - Customer Phone Number");
            Console.WriteLine("[4] - Return to Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Please enter a name:"); //Logic to grab user input
                    string name = Console.ReadLine();
                    try
                    {
                        List<Customer> listofCustomer = _customerBL.SearchCustomer("1", name); //logic to display result
                        foreach (var item in listofCustomer)
                        {
                            Console.WriteLine("--------------------");
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("Press the Enter key to continue.");
                        Console.ReadLine();
                        return "SearchCustomerMenu";
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Customer name could not be found.");
                        Console.WriteLine("Please press the Enter key to try again.");
                        Console.ReadLine();
                        return "SearchCustomerMenu";
                    }
                case "2":
                    Console.WriteLine("Please enter an email:"); 
                    string email = Console.ReadLine();
                    try
                    {
                        List<Customer> listofCustomer = _customerBL.SearchCustomer("2", email);
                        foreach (var item in listofCustomer)
                        {
                            Console.WriteLine("--------------------");
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("Press the Enter key to continue.");
                        Console.ReadLine();
                        return "SearchCustomerMenu";
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Customer information could not be found.");
                        Console.WriteLine("Please press the Enter key to try again.");
                        Console.ReadLine();
                        return "SearchCustomerMenu";
                    }
                case "3":
                    Console.WriteLine("Please enter phone number (must be 10 digits):"); 
                    string phone = Console.ReadLine();
                    try
                    {
                        List<Customer> listofCustomer = _customerBL.SearchCustomer("3", phone);
                        foreach (var item in listofCustomer)
                        {
                            Console.WriteLine("--------------------");
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("Press the Enter key to continue.");
                        Console.ReadLine();
                        return "SearchCustomerMenu";
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Customer information could not be found.");
                        Console.WriteLine("Please press the Enter key to try again.");
                        Console.ReadLine();
                        return "SearchCustomerMenu";
                    }
                case "4":
                    return "MainMenu";
                default:
                    Console.WriteLine("You've selected an invalid reponse.");
                    Console.WriteLine("Please press the Enter key to try again.");
                    Console.ReadLine();
                    return "SearchCustomerMenu";
            }
        }
    }
}
            
