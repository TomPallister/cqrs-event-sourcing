using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Messages;

namespace FirstOneTo.CommandProcessor
{
    internal sealed class CommandHandler
    {
        private readonly Dictionary<Type, IMessageHandler> _handlers;

        public CommandHandler()
        {
            _handlers = new Dictionary<Type, IMessageHandler>();
        }

        public void Add<TCommand>(Func<TCommand, IList<AbstractEvent>> handler) where TCommand : AbstractCommand
        {
            _handlers[typeof (TCommand)] = new MessageHandler<TCommand>(handler);
        }

        public IList<AbstractEvent> Handle<TCommand>(TCommand message) where TCommand : AbstractCommand
        {
            IMessageHandler handler = _handlers[message.GetType()];
            return  handler.Handle(message);
        }
    }
}