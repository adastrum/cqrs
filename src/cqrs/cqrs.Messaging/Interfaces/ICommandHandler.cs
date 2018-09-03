using System.Threading.Tasks;

namespace cqrs.Messaging.Interfaces
{
    public interface ICommandHandler<in TCommand>
        where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
