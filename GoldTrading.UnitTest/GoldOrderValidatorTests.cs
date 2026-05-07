using GoldTrading.Models;
using GoldTrading.Services;
using GoldTrading.Services.Interfaces;
using Moq;

namespace GoldTrading.UnitTest
{
    public class GoldOrderValidatorTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<IMarketService> _marketServiceMock;
        private readonly GoldOrderValidator _validator;

        public GoldOrderValidatorTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _marketServiceMock = new Mock<IMarketService>();
            _validator = new GoldOrderValidator(_customerServiceMock.Object, _marketServiceMock.Object);
        }

        [Fact]
        public void Validate_ValidBuyOrder_ReturnsIsValidTrue()
        {
            // Arrange
            var order = new OrderModel { CustomerId = "C001", OrderType = "buy", Quantity = 2.5m, QuotedPrice = 70000m };
            _marketServiceMock.Setup(m => m.GetCurrentMarketPrice()).Returns(70500m); // ต่างกันประมาณ 0.7% (ผ่านกฎ 2%)
            _customerServiceMock.Setup(c => c.GetAvailableBalance("C001")).Returns(200000m); // ต้องการ 175,000 (ผ่านกฎเงินพอ)

            // Act
            var result = _validator.Validate(order);

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Validate_InvalidOrderType_ReturnsError()
        {
            // Arrange
            var order = new OrderModel { CustomerId = "C001", OrderType = "trade", Quantity = 1.0m, QuotedPrice = 70000m };

            // Act
            var result = _validator.Validate(order);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("ประเภทคำสั่งซื้อต้องเป็น 'ซื้อ' หรือ 'ขาย'", result.Errors);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.5)]
        [InlineData(1.3)] // ไม่ใช่พหุคูณของ 0.5
        public void Validate_InvalidQuantity_ReturnsError(decimal invalidQuantity)
        {
            // Arrange
            var order = new OrderModel { CustomerId = "C001", OrderType = "sell", Quantity = invalidQuantity, QuotedPrice = 70000m };

            // Act
            var result = _validator.Validate(order);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Contains("ปริมาณต้องมากกว่าศูนย์") || e.Contains("ปริมาณต้องเป็นหน่วยที่ถูกต้อง"));
        }

        [Fact]
        public void Validate_BuyOrderWithInsufficientBalance_ReturnsError()
        {
            // Arrange
            var order = new OrderModel { CustomerId = "C001", OrderType = "buy", Quantity = 1.0m, QuotedPrice = 70000m };
            _marketServiceMock.Setup(m => m.GetCurrentMarketPrice()).Returns(70000m);
            _customerServiceMock.Setup(c => c.GetAvailableBalance("C001")).Returns(50000m); // เงินไม่พอ (มีแค่ 50,000 แต่ต้องใช้ 70,000)

            // Act
            var result = _validator.Validate(order);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Contains("ยอดเงินไม่เพียงพอ"));
        }

        [Theory]
        [InlineData(72000)] // แพงกว่าราคาตลาดเกิน 2%
        [InlineData(68000)] // ถูกกว่าราคาตลาดเกิน 2%
        public void Validate_StaleQuotedPrice_ReturnsError(decimal stalePrice)
        {
            // Arrange
            var order = new OrderModel { CustomerId = "C001", OrderType = "sell", Quantity = 1.0m, QuotedPrice = stalePrice };
            _marketServiceMock.Setup(m => m.GetCurrentMarketPrice()).Returns(70000m); // ราคาตลาดคือ 70,000

            // Act
            var result = _validator.Validate(order);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Contains("ราคาที่แจ้งไว้เป็นราคาปัจจุบัน"));
        }
    }
}
