using System.Collections.Generic;
using System.Threading.Tasks;

namespace KevPOS.EventStore
{
    public interface IEventDatabase
    {
        Task StoreAsync(EventData eventData);
        Task<IEnumerable<EventData>> FetchAsync(long aggregateId);
        void Store(EventData eventData);
        IEnumerable<EventData> Fetch(long aggregateId);
    }
}