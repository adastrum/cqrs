using cqrs.Messaging.Interfaces;

namespace cqrs.Messaging.Commands
{
    public class CreateUserCommand : ICommand
    {
        public CreateUserCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
