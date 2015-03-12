using FirstOneTo.Readmodel.Models;

namespace FirstOneTo.ReadModel.Infrastructure
{
    public interface IExampleDatabase
    {
        ExampleModel GetExample(long aggregateId);
        void StoreExample(ExampleModel exampleModel);
    }
}