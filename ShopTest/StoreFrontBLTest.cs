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
    }
}