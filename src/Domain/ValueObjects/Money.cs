using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public Money(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Amout cannot be < 0");
            Amount = amount;
        }

        public double Amount { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }
    }
}