using System;
using System.Threading.Tasks;
using cqrs.CommandStack.Commands;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.Common;
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

        public async Task<CommandResult> HandleAsync(CloseAuctionCommand command)
        {
            try
            {
                var auction = await _auctionRepository.FindOneAsync(command.AuctionId);

                auction.Close();

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
