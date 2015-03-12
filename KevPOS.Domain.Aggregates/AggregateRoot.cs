using System;
using System.Collections.Generic;
using KevPOS.Messages;
using KevPOS.ValueObjects;

namespace KevPOS.Domain.Aggregates
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly Dictionary<Type, Action<AbstractEvent>> _handlers;
        private readonly List<AbstractEvent> _uncommittedEvents;

        protected AggregateRoot(long id)
        {
            _handlers = new Dictionary<Type, Action<AbstractEvent>>();
            _uncommittedEvents = new List<AbstractEvent>();
            Id = new Id(id);
        }

        protected Id Id { get; set; }

        public IList<AbstractEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        public void MarkEventsCommitted()
        {
            _uncommittedEvents.Clear();
        }

        public void Update(AbstractEvent evt)
        {
            _handlers[evt.GetType()].Invoke(evt);
            _uncommittedEvents.Add(evt);
        }

        protected void HandlesEvent<T>(Action<T> handler) where T : AbstractEvent
        {
            _handlers.Add(typeof (T), evt => handler((T) evt));
        }

        public void LoadFrom(IEnumerable<AbstractEvent> events)
        {
            foreach (AbstractEvent evt in events)
            {
                _handlers[evt.GetType()].Invoke(evt);
            }
        }
    }
}