using ShopBL;
using ShopModel;

namespace ShopUI
{
    public class ViewInventory : IMenu
    {
        private List<Customer> _listofcustomer;

        public void Display()
        {
            foreach (var item in _listofcustomer)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine
        }
    }
}