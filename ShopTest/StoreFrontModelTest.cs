using ShopModel;
using Xunit;

namespace ShopTest
{
    public class StoreFrontModelTest
    {
        /// <summary>
        /// Checks the validation for a store's ID.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void StoreFrontIDShouldValidData()
        {
            //Arrange
            StoreFront store = new StoreFront();
            int validID = 1;

            //Act
            store.storeID = validID;

            //Assert
            Assert.NotNull(store.storeID);
            Assert.Equal(validID, store.storeID);
        }

        /// <summary>
        /// Checks the validation for the store's name.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void StoreFrontNameShouldValidData()
        {
            //Arrange
            StoreFront store = new StoreFront();
            string validName = "Colorado's Market";

            //Act
            store.Name = validName;

            //Assert
            Assert.NotNull(store.Name);
            Assert.Equal(validName, store.Name);
        }
    }
}