using System;

namespace KevPOS.CommandStore
{
    public class CommandData
    {
        public CommandData(long aggregateId, string data, string type, DateTime created)
        {
            AggregateId = aggregateId;
            Data = data;
            Type = type;
            Created = created;
            Version = 0;
        }

        public long AggregateId { get; private set; }
        public string Data { get; private set; }
        public string Type { get; private set; }
        public DateTime Created { get; private set; }
        public int Version { get; private set; }
    }
}