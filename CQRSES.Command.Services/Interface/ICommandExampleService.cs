using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstOneTo.Readmodel.Models;

namespace Tasks.Command.Services.Interface
{
    public interface ICommandExampleService
    {
        void Store(ExampleModel exampleModel);
    }
}
