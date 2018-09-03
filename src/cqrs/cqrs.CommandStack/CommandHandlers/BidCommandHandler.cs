using System.Threading.Tasks;
using cqrs.CommandStack.Commands;
using cqrs.Domain.Interfaces;
using cqrs.Domain.ValueObjects;
using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.CommandHandlers
{
    public class BidCommandHandler : ICommandHandler<BidCommand>
    {
        private readonly IAuctionRepository _auctionRepository;

        public BidCommandHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task HandleAsync(BidCommand command)
        {
            var auction = await _auctionRepository.FindOneAsync(command.AuctionId);

            auction.Bid(new Money(command.Amount), command.User);

            await _auctionRepository.UpdateAsync(auction);
        }
    }
}
