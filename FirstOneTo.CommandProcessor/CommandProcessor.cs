using System.Collections.Generic;
using FirstOneTo.Commands;
using FirstOneTo.Domain.Service.Interface;
using KevPOS.Messages;

namespace FirstOneTo.CommandProcessor
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly CommandHandler _commandHandler;

        public CommandProcessor(IExampleService exampleService)
        {
            _commandHandler = new CommandHandler();
            Bootstrap(exampleService);
        }

        public IList<AbstractEvent> Accept<TCommand>(TCommand command) where TCommand : AbstractCommand
        {
            return _commandHandler.Handle(command);
        }

        private void Bootstrap(IExampleService exampleService)
        {
            _commandHandler.Add<CreateExample>(command => Handlers.CreateExample(exampleService, command));
            _commandHandler.Add<ChangeExample>(command => Handlers.ChangeExample(exampleService, command));
        }
    }
}