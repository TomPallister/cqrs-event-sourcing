using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Domain.Aggregates;
using KevPOS.Messages;
using KevPOS.ValueObjects;

namespace KevPOS.EventRepository
{
    public interface IEventRepository
    {
        TAggregate Get<TAggregate>(Id aggregateId);
        IList<AbstractEvent> Store(AggregateRoot aggregate);
    }
}