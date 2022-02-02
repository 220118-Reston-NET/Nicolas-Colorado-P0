namespace ShopModel
{
    public class Product
    {
        public int productID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }


        public override string ToString()
        {
            return $"Name: {Name}\nPrice: {Price}\nCategory: {Category}";
        }
    }  
}