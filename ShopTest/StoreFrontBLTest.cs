using System.Collections.Generic;
using Moq;
using ShopBL;
using ShopDL;
using ShopModel;
using Xunit;

namespace ShopTest
{
    public class StoreFrontBLTest
    {
        [Fact]
        public void ShouldGetAllStore()
        {
            // Arrange
            int ID = 1;
            string sfName = "Colorado's Market";
            string sfAddress = "415 Las Vegas Boulevard, Las Vegas, NV";
            string sfPhone = "7298303837";

            StoreFront store = new StoreFront()
            {
                storeID = ID,
                Name = sfName,
                Address = sfAddress,
                Phone = sfPhone
            };

            List<StoreFront> expectedListOfStoreFront = new List<StoreFront>();
            expectedListOfStoreFront.Add(store);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetAllStoreFront()).Returns(expectedListOfStoreFront);

            IStoreFrontBL storeBL = new StoreFrontBL(mockRepo.Object);

            //Act
            List<StoreFront> actualListOfStoreFront = storeBL.GetAllStoreFront();

            // Assert
             Assert.Same(expectedListOfStoreFront, actualListOfStoreFront);
             Assert.Equal(ID, actualListOfStoreFront[0].storeID);
             Assert.Equal(sfName, actualListOfStoreFront[0].Name);
             Assert.Equal(sfAddress, actualListOfStoreFront[0].Address);
             Assert.Equal(sfPhone, actualListOfStoreFront[0].Phone);
        }

        [Fact]
        public void ShouldGetAllProducts()
        {
            //Arrange
            int ID = 1;
            string prodName = "Men's Goldlnk Chain";
            double prodPrice = 299.99;
            string prodCategory = "Jewelry and Precious Metals";
            int prodQuantity  = 2;

            Product product = new Product()
            {
                productID = ID,
                Name = prodName,
                Price = prodPrice,
                Category = prodCategory,
                Quantity = prodQuantity
            };

            List<Product> expectedListOfProduct = new List<Product>();
            expectedListOfProduct.Add(product);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetAllProducts()).Returns(expectedListOfProduct);

            IStoreFrontBL storeBL = new StoreFrontBL(mockRepo.Object);

            //Act
            List<Product> actualListOfProduct = storeBL.GetAllProducts();

            //Assert
             Assert.Same(expectedListOfProduct, actualListOfProduct);
             Assert.Equal(ID, actualListOfProduct[0].productID);
             Assert.Equal(prodName, actualListOfProduct[0].Name);
             Assert.Equal(prodPrice, actualListOfProduct[0].Price);
             Assert.Equal(prodCategory, actualListOfProduct[0].Category);
             Assert.Equal(prodQuantity, actualListOfProduct[0].Quantity);
        }
    }
}