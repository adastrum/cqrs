using System.Threading.Tasks;
using cqrs.CommandStack.Commands;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.CommandHandlers
{
    public class CloseAuctionCommandHandler : ICommandHandler<CloseAuctionCommand>
    {
        private readonly IAuctionRepository _auctionRepository;

        public CloseAuctionCommandHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task HandleAsync(CloseAuctionCommand command)
        {
            var auction = await _auctionRepository.FindOneAsync(command.AuctionId);

            auction.Close();

            await _auctionRepository.UpdateAsync(auction);
        }
    }
}
