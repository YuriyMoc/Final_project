using AutoServiceApp;

namespace FinalProjectTests
{
    public class UnitTest1
    {
        [Fact]
        public void TotalPrice_ShouldReturnSumOfAllServicePrices()
        {
            // Arrange
            var order = new Order();
            order.SelectedServices.Add(new Service("",15.0m));
            order.SelectedServices.Add(new Service("", 25.5m));
            order.SelectedServices.Add(new Service("", 9.5m));

            // Act
            decimal total = order.TotalPrice;

            // Assert
            Assert.Equal(50.0m, total);
        }
    }
}