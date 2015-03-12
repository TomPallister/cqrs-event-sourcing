using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Messages;

namespace KevPOS.InMemoryBus
{
    public class FakeBus : IEventPublisher
    {
        private readonly Dictionary<Type, List<Func<IMessage, Task>>> _routes;

        public FakeBus()
        {
            _routes = new Dictionary<Type, List<Func<IMessage, Task>>>();
        }

        public void Publish<T>(T evt) where T : AbstractEvent
        {
            List<Func<IMessage, Task>> handlers;
            //Type declaringType = typeof (T);
            if (_routes.TryGetValue(evt.GetType(), out handlers))
            {
                handlers.ForEach(action => action(evt));
            }
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