using System;

namespace KevPOS.EventStore
{
    public class EventData
    {
        public EventData(long aggregateId, string data, string type, DateTime created, int version)
        {
            AggregateId = aggregateId;
            Data = data;
            Type = type;
            Created = created;
            Version = version;
        }

        public long AggregateId { get; private set; }
        public string Data { get; private set; }
        public string Type { get; private set; }
        public DateTime Created { get; private set; }
        public int Version { get; private set; }
    }
}