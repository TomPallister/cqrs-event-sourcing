using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Messages;

namespace FirstOneTo.CommandProcessor
{
    internal class MessageHandler<T> : IMessageHandler where T : AbstractCommand
    {
        private readonly Func<T, IList<AbstractEvent>> _handler;

        public MessageHandler(Func<T, IList<AbstractEvent>> handler)
        {
            _handler = handler;
        }

        public IList<AbstractEvent> Handle(IMessage message)
        {
            return  _handler((T) message);
        }
    }
}