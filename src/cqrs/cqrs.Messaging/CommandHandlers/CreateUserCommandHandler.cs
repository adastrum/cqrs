using System;
using System.Threading.Tasks;
using cqrs.Domain.Entities;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.Commands;
using cqrs.Messaging.Common;
using cqrs.Messaging.Interfaces;

namespace cqrs.Messaging.CommandHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CommandResult> HandleAsync(CreateUserCommand command)
        {
            try
            {
                var user = new User(command.Name);

                await _userRepository.CreateAsync(user);

                return CommandResult.Successfull();
            }
            catch (Exception exception)
            {
                return CommandResult.Failed(exception.Message);
            }
        }
    }
}
