using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.EventsProcessor;
using KevPOS.Messages;

namespace FirstOneTo.EventsPublisher
{
    public class EventsPublisher : IEventsPublisher
    {
        private readonly IEventsProcessor _eventsProcessor;

        public EventsPublisher(IEventsProcessor eventsProcessor)
        {
            _eventsProcessor = eventsProcessor;
        }

        public void Publish(IList<AbstractEvent> events)
        {
            foreach (var abstractEvent in events)
            {
                 _eventsProcessor.Accept(abstractEvent);
            }
        }

        public void Publish(AbstractEvent evt)
        {
             _eventsProcessor.Accept(evt);
        }
    }
}