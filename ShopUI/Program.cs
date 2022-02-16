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

string _connectionString = configuration.GetConnectionString("ShopDBConnection");

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
            menu = new AddCustomerMenu(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "SearchCustomerMenu":
            Log.Information("Displaing SearchCustomer menu to user");
            menu = new SearchCustomerMenu(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "PlaceOrder":
            Log.Information("Displaying PlaceOrder menu to user");
            menu = new PlaceOrder(new CustomerBL(new SQLRepository(_connectionString)), new StoreFrontBL(new SQLRepository(_connectionString)), new OrderBL(new SQLRepository(_connectionString)));
            break;
        case "ViewCustomerOrder":
            Log.Information("Displaying ViewCustomerOrder menu to user");
            menu = new ViewCustomerOrder(new CustomerBL(new SQLRepository(_connectionString)));
            break;
        case "ViewStoreOrder":
            Log.Information("Displaying ViewStoreOrder menu to user");
            menu = new ViewStoreOrder(new StoreFrontBL(new SQLRepository(_connectionString)));
            break;
        case "ViewInventory":
            Log.Information("Displaying ViewStoreFront menu to user");
            menu = new ViewInventory(new StoreFrontBL(new SQLRepository(_connectionString)));
            break;
        case "Replenish":
            Log.Information("Displaying Replenish menu to user");
            menu = new Replenish(new StoreFrontBL(new SQLRepository(_connectionString)));
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
            Log.Warning("User selected an invalid response.");
            Console.WriteLine("Page does not exist!");
            Console.WriteLine("Please press the Enter button to continue");
            Console.ReadLine();
            Log.Information("User will restart the main menu and try again.");
            break;
    }
}

