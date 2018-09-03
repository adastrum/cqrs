using System.Threading.Tasks;
using cqrs.CommandStack.Commands;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.CommandHandlers
{
    public class CancelAuctionCommandHandler : ICommandHandler<CancelAuctionCommand>
    {
        private readonly IAuctionRepository _auctionRepository;

        public CancelAuctionCommandHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task HandleAsync(CancelAuctionCommand command)
        {
            var auction = await _auctionRepository.FindOneAsync(command.AuctionId);

            auction.Cancel();

            await _auctionRepository.UpdateAsync(auction);
        }
    }
}
