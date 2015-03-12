using System.Threading.Tasks;
using FirstOneTo.Handlers;
using KevPOS.Messages;
using KevPOS.ValueObjects;

namespace FirstOneTo.CommandSender
{
    public class CommandSender
    {
        private readonly CommandAndEventHandler _handler;

        public CommandSender(CommandAndEventHandler handler)
        {
            _handler = handler;
        }

        public ResponseContent Send(string commandName, AbstractCommand command)
        {
            return _handler.Process(commandName, command);
        }
    }
}