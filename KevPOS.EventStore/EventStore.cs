using System.Collections.Generic;
using System.Linq;

namespace KevPOS.EventStore
{
    public class EventStore
    {
        private readonly IEventDatabase _eventDatabase;

        public EventStore(IEventDatabase eventDatabase)
        {
            _eventDatabase = eventDatabase;
        }

        public void Write(EventData eventData)
        {
            _eventDatabase.Store(eventData);
        }

        public IEnumerable<EventData> Read(long id)
        {
            IEnumerable<EventData> events = _eventDatabase.Fetch(id);
            return events.OrderBy(evt => evt.Created);
        }
    }
}