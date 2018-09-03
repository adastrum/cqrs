using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.Commands
{
    public class CancelAuctionCommand : ICommand
    {
        public CancelAuctionCommand(string auctionId)
        {
            AuctionId = auctionId;
        }

        public string AuctionId { get; }
    }
}
