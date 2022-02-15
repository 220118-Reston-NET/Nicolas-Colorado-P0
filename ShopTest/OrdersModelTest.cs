using ShopModel;
using Xunit;

namespace ShopTest
{
    public class OrdersModelTest
    {
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