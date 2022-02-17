using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class ViewCustomerOrder : IMenu
    {
        private List<Customer> _listofCustomer;

        //Dependency Injection with CustomerBL
        private ICustomerBL _customerBL;
        public ViewCustomerOrder(ICustomerBL p_customerBL)
        {
            _customerBL = p_customerBL;
            _listofCustomer = _customerBL.GetAllCustomer();
        }

        public int _custID;

        public void Display()
        {
            //The menu displays a list of customers the user can choose from.
            Console.WriteLine("To display a customer's order history, you must enter their email.");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - Select a customer by email");
            Console.WriteLine("[2] - Return to Main Menu\n");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Log.Information("User selected to view a customer's order history.");
                    Console.WriteLine("First, please enter an email:"); 
                    string email = Console.ReadLine();
                    Log.Information("User entered in an email.");
                    try
                    {
                        List<Customer> listofCustomer = _customerBL.SearchCustomer("2", email);
                        foreach (var item in listofCustomer)
                        {
                            _custID = item.customerID;
                            Console.WriteLine("-------------------------");
                            Console.WriteLine(item);
                            Console.WriteLine("-------------------------");
                        }
                        Log.Information("Successfully retrieved and displayed customer information.");
                        Console.WriteLine("");
                        Console.WriteLine("Your customer information is displayed above. Press the Enter key to continue to the Order List:");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue:");
                        // //Gets the customer ID from the user.
                        // int custID = Convert.ToInt32(Console.ReadLine());
                        // Log.Information("User has entered a customer ID");
                        // if ((_listofCustomer.All(p => p.customerID != custID)))
                        // {
                        //     throw new Exception("Customer ID cannot be found.");
                        // }

                        //Displays the order history from the customer.
                        List<Orders> listofOrders = _customerBL.GetOrderbyCustomerID(_custID);
                        Console.WriteLine("=============== Order History ===============");
                        foreach (var item in listofOrders)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine("-------------------------");
                        }
                        Log.Information("Successfully retrieved and displayed list of orders from customer ID.");
                        Console.WriteLine("");
                        Console.WriteLine("Please press the Enter button to continue");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue.");
                    }
                    catch (System.Exception)
                    {
                        //Displays if user input has an invalid format.
                        Log.Warning("Customer email could not be found.");
                        Console.WriteLine("Customer email could not be found. Make sure you are spelling correctly");
                        Console.WriteLine("Press the Enter button to try again.");
                        Console.ReadLine();
                        Log.Information("User has pressed the Enter key to try again.");
                    }
                    return "ViewCustomerOrder";
                case "2":
                    Log.Information("User has selected to return to the main menu.");
                    return "MainMenu";
                default:
                    Log.Warning("User selected an invalid response.");
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to continue.");
                    Log.Information("User has pressed the Enter key to try again.");
                    return "ViewCustomerOrder";
            }
        }
    }
}