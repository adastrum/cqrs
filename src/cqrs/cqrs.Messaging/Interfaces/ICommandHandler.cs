using System.Threading.Tasks;
using cqrs.Messaging.Common;

namespace cqrs.Messaging.Interfaces
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<CommandResult> HandleAsync(TCommand command);
    }
}
