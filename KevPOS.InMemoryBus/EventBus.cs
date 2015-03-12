using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.EventsProcessor;
using KevPOS.Messages;

namespace KevPOS.InMemoryBus
{
    public class EventBus : IEventPublisher
    {
        private readonly IEventsProcessor _eventProcessor;
        private readonly Dictionary<Type, List<Func<IMessage, Task>>> _routes;

        public EventBus(IEventsProcessor eventProcessor)
        {
            _routes = new Dictionary<Type, List<Func<IMessage, Task>>>();
            _eventProcessor = eventProcessor;
        }

        public void Publish<T>(T evt) where T : AbstractEvent
        {
            _eventProcessor.Accept(evt);
            //List<Func<IMessage, Task>> handlers;
            //if (_routes.TryGetValue(@event.GetType(), out handlers))
            //{
            //    handlers.ForEach(action => action(@event));
            //}
        }

        public void RegisterHandler<T>(Func<T, Task> handler) where T : AbstractEvent
        {
            List<Func<IMessage, Task>> handlers;
            if (!_routes.TryGetValue(typeof (T), out handlers))
            {
                handlers = new List<Func<IMessage, Task>>();
                _routes.Add(typeof (T), handlers);
            }
            handlers.Add(DelegateAdjuster.CastArgument<IMessage, T>(evt => handler(evt)));
        }
    }
}