using ShopModel;
using Xunit;

namespace ShopTest
{
    public class CustomerModelTest
    {
        /// <summary>
        /// Checks the validation for number of digits in  customer's phone number
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void CustomerPhoneShouldSetValidData()
        {
            //Arrange
            Customer cust = new Customer();
            string validPhone = "4072797735";
        
            //Act
            cust.Phone = validPhone;

            //Assert
            Assert.NotNull(cust.Phone); 
            Assert.Equal(validPhone, cust.Phone);
        }

        /// <summary>
        /// Checks the validation for a customer's name.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void CustomerNameShouldValidData()
        {
            //Arrange
            Customer cust = new Customer();
            string validName = "Billy Bob";

            //Act
            cust.Name = validName;

            //Assert
            Assert.NotNull(cust.Name);
            Assert.Equal(validName, cust.Name);

        }

        /// <summary>
        /// Checks the validation for a customer's ID.
        /// Below is a unit test
        /// </summary>
        [Fact]
        public void CustomerIDShouldValidData()
        {
            //Arrange
            Customer cust = new Customer();
            int validID = 3;

            //Act
            cust.customerID = validID;

            //Assert
            Assert.NotNull(cust.customerID);
            Assert.Equal(validID, cust.customerID);
        }
    }
}
