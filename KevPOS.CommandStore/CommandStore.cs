using System.Threading.Tasks;

namespace KevPOS.CommandStore
{
    public class CommandStore
    {
        private readonly ICommandDatabase _commandDatabase;

        public CommandStore(ICommandDatabase commandDatabase)
        {
            _commandDatabase = commandDatabase;
        }

        public void Write(CommandData commandData)
        {
             _commandDatabase.Store(commandData);
        }
    }
}