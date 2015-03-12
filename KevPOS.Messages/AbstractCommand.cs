namespace KevPOS.Messages
{
    public abstract class AbstractCommand : IMessage
    {
        public long AggregateId { get; set; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}