using System;
using System.Threading.Tasks;
using cqrs.Messaging.Common;
using cqrs.Messaging.Interfaces;

namespace cqrs.Messaging.InMemory
{
    public class InMemoryBus : IBus
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandResult> SendCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_serviceProvider.GetService(typeof(ICommandHandler<TCommand>));
            return await handler.HandleAsync(command);
        }
    }
}
