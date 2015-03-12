using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KevPOS.EventStore
{
    public class InMemoryEventDatabase : IEventDatabase
    {
        private readonly IList<EventData> _events;

        public InMemoryEventDatabase(IList<EventData> store)
        {
            _events = store;
            _events.Add(new EventData(1, "{\"Name\":\"demo\",\"Password\":\"demo\", \"AggregateId\": 1}",
                "PlayerCreated", DateTime.Now, 0));
            _events.Add(new EventData(2, "{\"Name\":\"nonadmin\",\"Password\":\"nonadmin\", \"AggregateId\": 2}",
                "PlayerCreated", DateTime.Now, 0));
        }

        public InMemoryEventDatabase()
            : this(new List<EventData>())
        {
        }

        public IList<EventData> EventStore
        {
            get { return _events; }
        }

        public async Task<IEnumerable<EventData>> FetchAsync(long aggregateId)
        {
            return  await Task.Run(() => Fetch(aggregateId));
        }

        public async Task StoreAsync(EventData eventData)
        {
             await Task.Run(() => Store(eventData));
        }

        public void Store(EventData eventData)
        {
            _events.Add(eventData);
        }

        public IEnumerable<EventData> Fetch(long aggregateId)
        {
            IEnumerable<EventData> x = _events.Where(evt => evt.AggregateId == aggregateId);
            return x;
        }
    }
}