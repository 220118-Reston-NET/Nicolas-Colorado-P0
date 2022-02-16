using ShopModel;
using Xunit;

namespace ShopTest
{
    public class LineItemsModelTest
    {
        /// <summary>
        /// Checks the validation for a line item's quantity.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void ProductIDShouldValidData()
        {
            //Arrange
            LineItem item = new LineItem();
            int validQTY = 3;

            //Act
            item.Quantity = validQTY;

            //Assert
            Assert.NotNull(item.Quantity);
            Assert.Equal(validQTY, item.Quantity);
        }
    }
}