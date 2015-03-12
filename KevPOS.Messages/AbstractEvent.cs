namespace KevPOS.Messages
{
    public abstract class AbstractEvent : IMessage
    {
        public int Version { get; set; }

        public long AggregateId { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}