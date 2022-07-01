using DigitalWallet.Domain.ValueObjects;
using FluentAssertions;

namespace DigitalWallet.Domain.UnitTests.ValueObjects;
public class MoneyTests
{
    #region Initialize
    [Test]
    [TestCase(0)]
    [TestCase(15)]
    [TestCase(350000)]
    [TestCase(950000000)]
    public void InitializeShouldBeCorrectly(double initializeBalance)
    {
        var amount = new Money(initializeBalance);
        amount.Value.Should().Be(initializeBalance);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(-15)]
    [TestCase(-350000)]
    [TestCase(-950000000)]
    public void InitializeShouldThrowException(double initializeBalance)
    {
        // Amount should throw ArgumentOutOfRangeException.
        FluentActions.Invoking(() => new Money(initializeBalance))
            .Should().Throw<ArgumentOutOfRangeException>();
    }
    #endregion

    #region Increase
    [Test]
    [TestCase(0, 8, 8)]
    [TestCase(20, 13, 33)]
    [TestCase(250, 80, 330)]
    [TestCase(4300000, 2750000, 7050000)]
    [TestCase(45650000000, 38770000000, 84420000000)]
    public void IncreaseShouldCalculateCorrectly(double initializeAmount, double increaseAmount, double balance)
    {
        var initializeBalance = new Money(initializeAmount);
        initializeBalance = initializeBalance.Increase(increaseAmount);
        initializeBalance.Value.Should().Be(balance);
    }

    [Test]
    [TestCase(10, 5, 15)]
    [TestCase(250, 80, 330)]
    [TestCase(6700, 15500, 22200)]
    [TestCase(3750000, 1500000, 5250000)]
    public void IncreaseShouldCalculateCorrectlyWithMoney(double initializeAmount, double increaseAmount, double balance)
    {
        var initializeBalance = new Money(initializeAmount);
        initializeBalance = initializeBalance.Increase(new Money(increaseAmount));
        initializeBalance.Value.Should().Be(balance);
    }

    [Test]
    [TestCase(0, -1)]
    [TestCase(5, -1)]
    [TestCase(85, -24)]
    [TestCase(8500, -3450)]
    [TestCase(350000, -81000)]
    [TestCase(2650000, -700000)]
    [TestCase(974000000, -175000000)]
    public void IncreaseShouldThrowException(double initializeAmount, double cost)
    {
        var initializeBalance = new Money(initializeAmount);
        FluentActions.Invoking(() => initializeBalance.Increase(cost))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(0, -1)]
    [TestCase(5, -1)]
    [TestCase(85, -24)]
    [TestCase(8500, -3450)]
    [TestCase(350000, -81000)]
    [TestCase(2650000, -700000)]
    [TestCase(974000000, -175000000)]
    public void IncreaseShouldThrowExceptionWithMoney(double initializeAmount, double cost)
    {
        var initializeBalance = new Money(initializeAmount);
        FluentActions.Invoking(() => initializeBalance.Increase(new Money(cost)))
            .Should().Throw<ArgumentOutOfRangeException>();
    }
    #endregion

    #region Decrease
    [Test]
    [TestCase(20, 13, 7)]
    [TestCase(250, 80, 170)]
    [TestCase(4300000, 2750000, 1550000)]
    [TestCase(45650000000, 38770000000, 6880000000)]
    public void DecreaseShouldCalculateCorrectly(double initializeAmount, double cost, double balance)
    {
        var initializeBalance = new Money(initializeAmount);
        initializeBalance = initializeBalance.Decrease(cost);
        initializeBalance.Value.Should().Be(balance);
    }

    [Test]
    [TestCase(20, 13, 7)]
    [TestCase(250, 80, 170)]
    [TestCase(4300000, 2750000, 1550000)]
    [TestCase(45650000000, 38770000000, 6880000000)]
    public void DecreaseShouldCalculateCorrectlyWithMoney(double initializeAmount, double cost, double balance)
    {
        var initializeBalance = new Money(initializeAmount);
        initializeBalance = initializeBalance.Decrease(new Money(cost));
        initializeBalance.Value.Should().Be(balance);
    }

    [Test]
    [TestCase(5, 6)]
    [TestCase(0, 8)]
    [TestCase(350000, 480000)]
    public void DecreaseShouldThrowException(double initializeAmount, double cost)
    {
        // Initialize amount.
        var initializeBalance = new Money(initializeAmount);
        // Amount should throw ArgumentOutOfRangeException.
        FluentActions.Invoking(() => initializeBalance.Decrease(cost))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(5, 6)]
    [TestCase(0, 8)]
    [TestCase(350000, 480000)]
    public void DecreaseShouldThrowExceptionWithMoney(double initializeAmount, double cost)
    {
        // Initialize amount to 20.
        var initializeBalance = new Money(initializeAmount);
        // Amount should throw ArgumentOutOfRangeException.
        FluentActions.Invoking(() => initializeBalance.Decrease(new Money(cost)))
            .Should().Throw<ArgumentOutOfRangeException>();
    }
    #endregion
}