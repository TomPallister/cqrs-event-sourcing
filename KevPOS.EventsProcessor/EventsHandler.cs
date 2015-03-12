using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Messages;
using log4net;

namespace KevPOS.EventsProcessor
{
    internal sealed class EventsHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (EventsHandler));

        private readonly Dictionary<Type, IMessageHandler> _handlers;

        public EventsHandler()
        {
            _handlers = new Dictionary<Type, IMessageHandler>();
        }

        public void Add<TEvent>(Action<TEvent> handler) where TEvent : AbstractEvent
        {
            _handlers[typeof (TEvent)] = new MessageHandler<TEvent>(handler);
        }

        public void Handle<TEvent>(TEvent message) where TEvent : AbstractEvent
        {
            IMessageHandler handler = _handlers[message.GetType()];
             handler.Handle(message);
        }
    }
}