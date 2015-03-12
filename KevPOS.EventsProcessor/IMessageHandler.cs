using System.Threading.Tasks;
using KevPOS.Messages;

namespace KevPOS.EventsProcessor
{
    internal interface IMessageHandler
    {
        void Handle(IMessage message);
    }
}