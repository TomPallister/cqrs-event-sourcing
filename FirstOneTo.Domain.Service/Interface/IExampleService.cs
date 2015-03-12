using System.Collections.Generic;
using System.Threading.Tasks;
using FirstOneTo.Commands;
using KevPOS.Messages;

namespace FirstOneTo.Domain.Service.Interface
{
    public interface IExampleService
    {
        IList<AbstractEvent> CreateExample(CreateExample createExampleCommand);
        IList<AbstractEvent> ChangeExample(ChangeExample changeExampleCommand);

    }
}