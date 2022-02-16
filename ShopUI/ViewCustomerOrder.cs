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

        public void Display()
        {
            //The menu displays a list of customers the user can choose from.
            Console.WriteLine("Displayed below is a list of customers currently in our database. To view a customer's order history, enter their ID.\n");
            Console.WriteLine("=============== Customer List ===============");
            foreach (var item in _listofCustomer)
            {
                Console.WriteLine("====================");
                Console.WriteLine(item);
            }
            Console.WriteLine("");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] - Select a customer by ID");
            Console.WriteLine("[2] - Return to Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Log.Information("User selected to view a customer's order history.");
                    Console.WriteLine("Please enter customer's ID:");
                    try
                    {
                        //Gets the customer ID from the user.
                        int customerID = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User has entered a customer ID");

                        //Displays the order history from the customer.
                        List<Orders> listofOrders = _customerBL.GetOrderbyCustomerID(customerID);
                        foreach (var item in listofOrders)
                        {
                            Console.WriteLine("====================");
                            Console.WriteLine(item);
                        }
                        Log.Information("Successfully retrieved and displayed list of orders from customer ID.");
                        Console.WriteLine("Please press the Enter button to continue");
                        Console.ReadLine();
                        Log.Information("User pressed the Enter key to continue.");
                    }
                    catch (FormatException)
                    {
                        //Displays if user input has an invalid format.
                        Log.Warning("User inputted a invalid value for customer ID.");
                        Console.WriteLine("You've selected an invalid response.");
                        Console.WriteLine("Press the Enter button to continue.");
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