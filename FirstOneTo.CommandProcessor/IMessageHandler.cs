using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Messages;

namespace FirstOneTo.CommandProcessor
{
    internal interface IMessageHandler
    {
        IList<AbstractEvent> Handle(IMessage message);
    }
}