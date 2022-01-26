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
                    Console.WriteLine("Please enter a name:");
                    //if (_newCustomer.Name == userInput)










           // }
            


        //}
    }

    
}