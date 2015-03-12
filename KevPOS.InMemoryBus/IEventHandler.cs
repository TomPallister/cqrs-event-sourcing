using KevPOS.Messages;

namespace KevPOS.InMemoryBus
{
    public interface IHandle<in T> where T : AbstractEvent
    {
        void Handle(T evt);
    }
}