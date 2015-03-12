using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.Readmodel.Models;
using Tasks.Command.Services.Interface;

namespace Tasks.Command.Services.Concrete
{
    public class CommandExampleService : ICommandExampleService
    {
        private readonly IExampleDatabase _exampleDatabase;

        public CommandExampleService(IExampleDatabase exampleDatabase)
        {
            _exampleDatabase = exampleDatabase;
        }

        public void Store(ExampleModel exampleModel)
        {
            _exampleDatabase.StoreExample(exampleModel);
        }
    }
}