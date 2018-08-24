using System;
using cqrs.Domain.Common;
using cqrs.Domain.ValueObjects;

namespace cqrs.Domain.Entities
{
    public class Bid : Entity
    {
        public Money Amount { get; protected set; }
        public DateTime Date { get; protected set; }
        public User Bidder { get; protected set; }

        protected Bid() { }

        public Bid(Money amount, User bidder)
        {
            Amount = amount;
            Date = DateTime.Now;
            Bidder = bidder;
        }
    }
}
