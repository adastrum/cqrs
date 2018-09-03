using cqrs.Messaging.Interfaces;

namespace cqrs.CommandStack.Commands
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
