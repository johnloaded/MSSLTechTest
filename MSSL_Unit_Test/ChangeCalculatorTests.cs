using Xunit;
using Moq;
using MSSL_Business_Logic_Layer;

namespace MSSL_Unit_Test
{
    public class ChangeCalculatorTests
    {
        private readonly IChangeCalculator _changeCalculator;
        private readonly Mock<ICurrency> _currency = new Mock<ICurrency>();

        public ChangeCalculatorTests()
        {
            _changeCalculator = new ChangeCalculator(_currency.Object);
        }

        [Fact]
        public void ChangeContainsCorrectNumberOfDenominations()
        {
            // Arrange
            
            // Act
            var transactionChange = _changeCalculator.CalculateChange(20.00, 5.50);

            // Assert
            Assert.Equal(3, transactionChange.DenominationCount.Count);
        }

        [Theory]
        [InlineData(20.00, 5.50, 3)]
        [InlineData(20.00, 6.50, 4)]
        public void ChangeContainsCorrectNumberOfDenominationsMultiple(double amount, double purchasePrice, int expected)
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(amount, purchasePrice);

            // Assert
            Assert.Equal(expected, transactionChange.DenominationCount.Count);
        }

        [Fact]
        public void ChangeContainsTenNoteDenomination()
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(20, 5.50);
            var expected = 10;

            // Assert
            Assert.Contains<DenominationCount>(transactionChange.DenominationCount.Where(x => x.Denomination.Equals(expected)), item => true);
        }


        [Fact]
        public void ChangeContainsOneTenNoteDenomination()
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(20, 5.50);
            var denomination = 10;
            var count = 1;

            // Assert
            Assert.Contains<DenominationCount>(transactionChange.DenominationCount.Where(x => x.Denomination.Equals(denomination) && x.Count.Equals(count)), item => true);
        }

        [Fact]
        public void ChangeContainsTwoNoteDenomination()
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(20, 5.50);
            var expected = 2;

            // Assert
            Assert.Contains<DenominationCount>(transactionChange.DenominationCount.Where(x => x.Denomination.Equals(expected)), item => true);
        }

        [Fact]
        public void ChangeContainsTwoTwoNoteDenomination()
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(20, 5.50);
            var denomination = 2;
            var count = 2;

            // Assert
            Assert.Contains<DenominationCount>(transactionChange.DenominationCount.Where(x => x.Denomination.Equals(denomination) && x.Count.Equals(count)), item => true);
        }


        [Fact]
        public void ChangeContainsFiftyCoinDenomination()
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(20, 5.50);
            var expected = 0.50;

            // Assert
            Assert.Contains<DenominationCount>(transactionChange.DenominationCount.Where(x => x.Denomination.Equals(expected)), item => true);
        }

        [Fact]
        public void ChangeContainsOneFiftyCoinDenomination()
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(20, 5.50);
            var denomination = 0.50;
            var count = 1;

            // Assert
            Assert.Contains<DenominationCount>(transactionChange.DenominationCount.Where(x => x.Denomination.Equals(denomination) && x.Count.Equals(count)), item => true);
        }

        [Fact]
        public void ChangeDoesNotContainsFiveNoteDenomination()
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(20, 5.50);
            var expected = 5;

            // Assert
            Assert.Contains<DenominationCount>(transactionChange.DenominationCount.Where(x => !x.Denomination.Equals(expected)), item => true);
        }

        [Theory]
        [InlineData(20.00, 5.50, true)]
        [InlineData(20.00, 20.50, false)]
        public void CheckIfThereIsChangeToReturn(double amount, double purchasePrice, bool expected)
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(amount, purchasePrice);

            // Assert
            Assert.Equal(expected, !transactionChange.DenominationCount.Count.Equals(0));
        }

        [Theory]
        [InlineData(20.00, 5.50, 10)]
        [InlineData(20.00, 5.50, 2)]
        [InlineData(20.00, 5.50, 0.5)]
        public void ChangeContainsDenomination(double amount, double purchasePrice, double expected)
        {
            // Arrange

            // Act
            var transactionChange = _changeCalculator.CalculateChange(amount, purchasePrice);

            // Assert
            Assert.Contains(transactionChange.DenominationCount, item => Math.Abs(item.Denomination - expected) < 0.0000001);
        }
    }
}