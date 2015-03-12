using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.Readmodel.Models;
using FirstOneTo.Readmodel.Services.Interface;

namespace FirstOneTo.Readmodel.Services.Concrete
{
    public class QueryExampleService : IQueryExampleService
    {
        private readonly IExampleDatabase _readModelExampleDatabase;

        public QueryExampleService(IExampleDatabase readModelExampleDatabase)
        {
            _readModelExampleDatabase = readModelExampleDatabase;
        }

        public ExampleModel GetExample(long aggregateId)
        {
            return _readModelExampleDatabase.GetExample(aggregateId);
        }

        public void StoreExample(ExampleModel exampleModel)
        {
            _readModelExampleDatabase.StoreExample(exampleModel);
        }
    }
}