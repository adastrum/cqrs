using cqrs.Domain.Entities;
using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.Commands
{
    public class BidCommand : ICommand
    {
        public BidCommand(string auctionId, decimal amount, User user)
        {
            AuctionId = auctionId;
            Amount = amount;
            User = user;
        }

        public string AuctionId { get; }
        public decimal Amount { get; }
        public User User { get; }
    }
}
