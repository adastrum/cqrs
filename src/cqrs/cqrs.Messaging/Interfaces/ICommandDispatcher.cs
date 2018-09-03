using System.Threading.Tasks;

namespace cqrs.Messaging.Interfaces
{
    public interface ICommandDispatcher
    {
        Task PublishAsync<TCommand>(TCommand command)
            where TCommand : class, ICommand;
    }
}
