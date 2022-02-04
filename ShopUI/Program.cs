global using Serilog;
using Microsoft.Extensions.Configuration;
using ShopBL;
using ShopDL;
using ShopUI;


//Create and configure the logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(".logs/user.txt") //Configures logger to save file
    .CreateLogger();

//Reading and obtaining the connection strings from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string _connectionStrings = configuration.GetConnectionString("ShopDBConnection");

bool repeat = true;
IMenu menu = new MainMenu();

while (repeat)
{
    Console.Clear();
    menu.Display();
    string ans = menu.UserChoice();

    switch (ans)
    {
        case "AddCustomerMenu":
            Log.Information("Displaying AddCustomer menu to user");
            menu = new AddCustomerMenu(new CustomerBL(new SQLRepository(_connectionStrings)));
            break;
        case "SearchCustomerMenu":
            Log.Information("Displaing SearchCustomer menu to user");
            menu = new SearchCustomer(new CustomerBL(new SQLRepository(_connectionStrings)));
            break;
        case "PlaceOrder":
            Log.Information("Displaying PlaceOrder menu to user");
            menu = new PlaceOrder(new CustomerBL(new Repository()));
            break;
        case "ViewOrder":
            Log.Information("Displaying ViewOrder menu to user");
            menu = new ViewOrder(new CustomerBL(new Repository()));
            break;
        case "ViewStoreFront":
            Log.Information("Displaying ViewStoreFront menu to user");
            menu = new ViewStoreFront(new CustomerBL(new Repository()));
            break;
        case "Replenish":
            Log.Information("Displaying Replenish menu to user");
            menu = new Replenish(new CustomerBL(new Repository()));
            break;
        case "MainMenu":
            Log.Information("Displaying MainMenu to user");
            menu = new MainMenu();
            break;
        case "Exit":
            Log.Information("Exiting application");
            Log.CloseAndFlush(); //Closes our logger resource
            repeat = false;
            break;
        default:
            Console.WriteLine("Page does not exist!");
            Console.WriteLine("Please press the Enter button to continue");
            Console.ReadLine();
            break;
    }
}

