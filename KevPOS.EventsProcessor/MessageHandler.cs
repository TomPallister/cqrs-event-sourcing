using System;
using System.Threading.Tasks;
using KevPOS.Messages;

namespace KevPOS.EventsProcessor
{
    internal class MessageHandler<T> : IMessageHandler where T : AbstractEvent
    {
        private readonly Action<T> _handler;

        public MessageHandler(Action<T> handler)
        {
            _handler = handler;
        }

        public void Handle(IMessage message)
        {
             _handler((T) message);
        }
    }
}