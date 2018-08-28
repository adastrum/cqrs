using System;
using System.Threading.Tasks;
using cqrs.Domain.Entities;
using cqrs.Domain.Interfaces;
using cqrs.Domain.ValueObjects;
using cqrs.Messaging.Commands;
using cqrs.Messaging.Common;
using cqrs.Messaging.Interfaces;

namespace cqrs.Messaging.CommandHandlers
{
    public class CreateAuctionCommandHandler : ICommandHandler<CreateAuctionCommand>
    {
        private readonly IAuctionRepository _auctionRepository;

        public CreateAuctionCommandHandler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<CommandResult> HandleAsync(CreateAuctionCommand command)
        {
            try
            {
                var auction = new Auction(command.Name, command.Description, new TimeSpan(command.Days, command.Hours, command.Minutes, 0), new Money(command.InitialAmount), command.Seller);
                auction.Start();

                await _auctionRepository.CreateAsync(auction);

                return CommandResult.Successfull();
            }
            catch (Exception exception)
            {
                return CommandResult.Failed(exception.Message);
            }
        }
    }
}
