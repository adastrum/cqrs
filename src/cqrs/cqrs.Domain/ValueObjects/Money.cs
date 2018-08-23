using System;
using cqrs.Domain.Enums;

namespace cqrs.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; protected set; }
        public Currency Currency { get; protected set; }

        public static bool operator <(Money left, Money right)
        {
            if (left == null) throw new ArgumentException(nameof(left));
            if (right == null) throw new ArgumentException(nameof(right));

            return left.Amount < right.Amount;
        }

        public static bool operator >(Money left, Money right)
        {
            if (left == null) throw new ArgumentException(nameof(left));
            if (right == null) throw new ArgumentException(nameof(right));

            return left.Amount > right.Amount;
        }

        public static bool operator <=(Money left, Money right)
        {

            return left.Amount < right.Amount || left.Amount == right.Amount;
        }

        public static bool operator >=(Money left, Money right)
        {

            return left.Amount > right.Amount || left.Amount == right.Amount;
        }

        public Money(decimal amount)
        {
            Amount = amount;
            Currency = Currency.RUB;
        }
    }
}
