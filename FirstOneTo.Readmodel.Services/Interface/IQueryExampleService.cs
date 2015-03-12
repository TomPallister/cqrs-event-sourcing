using FirstOneTo.Readmodel.Models;

namespace FirstOneTo.Readmodel.Services.Interface
{
    public interface IQueryExampleService
    {
        ExampleModel GetExample(long aggregateId);
    }
}