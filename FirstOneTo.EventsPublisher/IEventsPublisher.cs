using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Messages;

namespace FirstOneTo.EventsPublisher
{
    public interface IEventsPublisher
    {
        void Publish(IList<AbstractEvent> events);
        void Publish(AbstractEvent events);
    }
}