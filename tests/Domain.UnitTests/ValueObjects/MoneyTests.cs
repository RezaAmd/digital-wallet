using Domain.ValueObjects;
using FluentAssertions;

namespace Wallet.Domain.UnitTests.ValueObjects;
public class MoneyTests
{
    [Test]
    public void InitializeShouldBeZero()
    {
        var amount = new Money();
        amount.Value.Should().Be(0);
    }

    [Test]
    public void IncreaseShouldCalculateCorrectly()
    {
        // Initialize amount to 50.
        var amount = new Money(50);
        // Increase 60 to amount.
        amount = amount.Increase(new Money(60));
        // Value should be 110.
        amount.Value.Should().Be(110);
    }

    [Test]
    public void DecreaseShouldCalculateCorrectly()
    {
        // Initialize amount to 327.
        var amount = new Money(327);
        // Decrease 148 from amount.
        amount = amount.Decrease(new Money(148));
        // Value should be 179.
        amount.Value.Should().Be(179);
    }

    [Test]
    public void DecreaseShouldThrowException()
    {
        // Initialize amount to 20.
        var amount = new Money(20);
        // Amount should throw ArgumentOutOfRangeException.
        FluentActions.Invoking(() => amount.Decrease(new Money(148)))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(-80)]
    [TestCase(-74598)]
    public void InitializeShouldThrowException(double amount)
    {
        // Amount should throw ArgumentOutOfRangeException.
        FluentActions.Invoking(() => new Money(amount))
            .Should().Throw<ArgumentOutOfRangeException>();
    }
}