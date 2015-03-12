using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KevPOS.Domain.Aggregates;
using KevPOS.EventStore;
using KevPOS.Messages;
using KevPOS.Serialisation.Infrasturcture;
using KevPOS.TypeExtensions.Infrastructure;
using KevPOS.ValueObjects;

namespace KevPOS.EventRepository
{
    public class EventRepository : IEventRepository
    {
        //private readonly IEventPublisher _eventPublisher;
        private readonly EventStore.EventStore _eventStore;
        private readonly IMessageResolver _resolver;

        public EventRepository(EventStore.EventStore eventStore, IMessageResolver resolver
            //IEventPublisher eventPublisher
            )
        {
            _eventStore = eventStore;
            _resolver = resolver;
            //_eventPublisher = eventPublisher;
        }

        public EventStore.EventStore EventStore
        {
            get { return _eventStore; }
        }

        public IList<AbstractEvent> Store(AggregateRoot aggregate)
        {

            IList<AbstractEvent> events = aggregate.GetUncommittedEvents();
            foreach (var abstractEvent in events)
            {
                string serialisedEvent = JsonSerialiser.Serialise(abstractEvent);
                string typeName = abstractEvent.GetType().Name;
                var eventData = new EventData(abstractEvent.AggregateId, serialisedEvent, typeName, DateTime.Now, abstractEvent.Version);
                 _eventStore.Write(eventData);
            }
            return events;
        }

        public  TAggregate Get<TAggregate>(Id aggregateId)
        {
            IEnumerable<EventData> rawEvents =  _eventStore.Read(aggregateId.ToLong());
            EventData[] eventDatas = rawEvents as EventData[] ?? rawEvents.ToArray();

            var events = new List<AbstractEvent>();

            if (eventDatas.Length == 0)
            {
                throw new Exception("The aggregate being loaded does not have any events");
            }
            eventDatas.Each(evt =>
            {
                Type type = GetTypeForEvent(evt.Type);
                AbstractEvent e = JsonSerialiser.Deserialise(evt.Data, type);
                events.Add(e);
            });
            var aggregate = (TAggregate) Activator.CreateInstance(typeof (TAggregate), aggregateId.ToLong(), events);
            return aggregate;
        }

        private Type GetTypeForEvent(string typeName)
        {
            Type resolvedType = _resolver.TypeForName(typeName);
            return resolvedType;
        }
    }
}