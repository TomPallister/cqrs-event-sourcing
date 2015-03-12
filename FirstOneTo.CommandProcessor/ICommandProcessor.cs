using System.Collections.Generic;
using System.Threading.Tasks;
using KevPOS.Messages;

namespace FirstOneTo.CommandProcessor
{
    public interface ICommandProcessor
    {
        IList<AbstractEvent> Accept<TCommand>(TCommand command) where TCommand : AbstractCommand;
    }
}