using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Messages;

namespace KevPOS.EventsProcessor
{
    public interface IEventsProcessor
    {
        void Accept<TEvent>(TEvent evt) where TEvent : AbstractEvent;
        void Accept<TEvent>(List<TEvent> events) where TEvent : AbstractEvent;
    }
}