namespace ShopModel
{
    public class Product
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }


        public override string ToString()
        {
            return $"Name: {Name}\nPrice: {Price}\nCategory: {Category}\nDescription: {Description}";
        }
    }  
}