using System;
using System.Threading.Tasks;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.Commands;
using cqrs.Messaging.Common;
using cqrs.Messaging.Interfaces;

namespace cqrs.Messaging.CommandHandlers
{
    public class CancelAuctionCommandHandler : ICommandHandler<CancelAuctionCommand>
    {
        private readonly IAuctionRepository _auctionRepository;

        public CancelAuctionCommandHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<CommandResult> HandleAsync(CancelAuctionCommand command)
        {
            try
            {
                var auction = await _auctionRepository.FindOneAsync(command.AuctionId);

                auction.Cancel();

                await _auctionRepository.UpdateAsync(auction);

                return CommandResult.Successfull();
            }
            catch (Exception exception)
            {
                return CommandResult.Failed(exception.Message);
            }
        }
    }
}
