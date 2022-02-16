using ShopModel;
using Xunit;

namespace ShopTest
{
    public class OrdersModelTest
    {
        /// <summary>
        /// Checks the validation for a order's ID.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void OrderIDShouldValidData()
        {
            //Arrange
            Orders order = new Orders();
            int validID = 4;

            //Act
            order.orderID = validID;

            //Assert
            Assert.NotNull(order.orderID);
            Assert.Equal(validID, order.orderID);
        }

        /// <summary>
        /// Checks the validation for a order's total price.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void TotalPriceShouldSetValidData()
        {
            //Arrange
            Orders order = new Orders();
            double validTotalPrice = 321.09;

            //Act
            order.TotalPrice = validTotalPrice;

            //Assert
            Assert.NotNull(order.TotalPrice);
            Assert.Equal(validTotalPrice, order.TotalPrice);
        }
    }
}