using ShopModel;
using Xunit;

namespace ShopTest
{
    public class ProductsModelTest
    {
        /// <summary>
        /// Checks the validation for a product's ID.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void ProductIDShouldValidData()
        {
            //Arrange
            Product product = new Product();
            int validID = 3;

            //Act
            product.productID = validID;

            //Assert
            Assert.NotNull(product.productID);
            Assert.Equal(validID, product.productID);
        }

        /// <summary>
        /// Checks the validation for a product's name.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void ProductNameShouldValidData()
        {
            //Arrange
            Product product = new Product();
            string validName = "Banana";

            //Act
            product.Name = validName;

            //Assert
            Assert.NotNull(product.Name);
            Assert.Equal(validName, product.Name);
        }

        /// <summary>
        /// Checks the validation for a product's price.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void ProductPriceShouldValidData()
        {
            //Arrange
            Product product = new Product();
            double validPrice = 1.99;

            //Act
            product.Price = validPrice;

            //Assert
            Assert.NotNull(product.Price);
            Assert.Equal(validPrice, product.Price);
        }
    }
}

