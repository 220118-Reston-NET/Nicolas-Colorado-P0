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
            _listofCustomer = _customerBL.ViewCustomerOrder();
        }

        public void Display()
        {
            foreach (var item in _listofCustomer)
            {
                Console.WriteLine("====================");
                Console.WriteLine(item);
            }
            
            Console.WriteLine("Displayed above is a list of customers currently in our database. To view a customer's order history, enter their ID.");
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
                    Console.WriteLine("Please enter customer's ID:");
                    try
                    {
                        int customerID = Convert.ToInt32(Console.ReadLine());
                        List<Orders> listofOrders = _customerBL.GetOrderbyCustomerID(customerID);
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
                        return "ViewCustomerOrder";  
                    }
                case "2":
                    return "MainMenu";
                default:
                    Console.WriteLine("You've selected an invalid response.");
                    Console.WriteLine("Press the Enter button to continue.");
                    return "ViewCustomerOrder";
            }
        }
    }
}