using ShopBL;
using ShopDL;
using ShopUI;

Console.WriteLine("Hello, World!");

bool repeat = true;
IMenu menu = new MainMenu();

while (repeat)
{
    Console.Clear();
    menu.Display();
    string ans = menu.UserChoice();

    switch (ans)
    {
        case "AddObject":
            menu = new AddCustomerMenu();
            break;
        case "MainMenu":
            menu = new MainMenu();
            break;
        case "Exit":
            repeat = false;
            break;
        default:
            Console.WriteLine("Page does not exist!");
            break;
    }
}

