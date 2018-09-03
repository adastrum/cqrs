using System.Threading.Tasks;
using cqrs.CommandStack.Commands;
using cqrs.Domain.Entities;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.CommandHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var user = new User(command.Name);

            await _userRepository.CreateAsync(user);
        }
    }
}
