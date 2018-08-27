using System;
using System.Threading.Tasks;
using cqrs.Domain.Interfaces;
using cqrs.Domain.ValueObjects;
using cqrs.Messaging.Commands;
using cqrs.Messaging.Common;
using cqrs.Messaging.Interfaces;

namespace cqrs.Messaging.CommandHandlers
{
    public class BidCommandHandler : ICommandHandler<BidCommand>
    {
        private readonly IAuctionRepository _auctionRepository;

        public BidCommandHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<CommandResult> HandleAsync(BidCommand command)
        {
            try
            {
                var auction = await _auctionRepository.FindOneAsync(command.AuctionId);

                auction.Bid(new Money(command.Amount), command.User);

                await _auctionRepository.UpdateAsync(auction);

                return CommandResult.Successfull();
            }
            catch(Exception exception)
            {
                return CommandResult.Failed(exception.Message);
            }
        }
    }
}
