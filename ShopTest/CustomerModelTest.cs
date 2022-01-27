using ShopModel;
using Xunit;

namespace ShopTest;

public class CustomerModelTest
{
    /// <summary>
    /// Checks the validation for number of digits in  customer's phone number
    /// Below is a unit test
    /// </summary>
    [Fact]
    public void PhoneShouldSetValidData()
    {
        //Arrange
        Customer cust = new Customer();
        int validPhone = 10;
        
        //Act
        cust.Phone = validPhone;

        //Assert
        Assert.NotNull(cust); 
        Assert.Equal(validPhone, cust.Phone);
    }

}