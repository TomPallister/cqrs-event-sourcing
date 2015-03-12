using System.Collections.Generic;
using KevPOS.Messages;

namespace KevPOS.Domain.Aggregates
{
    public interface IAggregateRoot
    {
        IList<AbstractEvent> GetUncommittedEvents();
        void MarkEventsCommitted();
    }
}