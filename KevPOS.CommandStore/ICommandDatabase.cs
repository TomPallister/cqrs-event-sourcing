using System.Threading.Tasks;

namespace KevPOS.CommandStore
{
    public interface ICommandDatabase
    {
        void Store(CommandData commandData);
    }
}