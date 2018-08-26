using System.Threading.Tasks;
using cqrs.Messaging.Common;

namespace cqrs.Messaging.Interfaces
{
    public interface IBus
    {
        Task<CommandResult> SendCommandAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
