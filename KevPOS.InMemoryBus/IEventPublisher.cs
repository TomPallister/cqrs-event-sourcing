using KevPOS.Messages;

namespace KevPOS.InMemoryBus
{
    public interface IEventPublisher
    {
        void Publish<T>(T evt) where T : AbstractEvent;
    }
}