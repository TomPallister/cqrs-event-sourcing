using System.Collections.Generic;
using System.Linq;
using FirstOneTo.Readmodel.Models;

namespace FirstOneTo.ReadModel.Infrastructure.InMemory
{
    public class InMemoryExampleDatabase : IExampleDatabase
    {
        private readonly List<ExampleModel> _acceptedChallenges = new List<ExampleModel>();

        public ExampleModel GetExample(long aggregateId)
        {
            return _acceptedChallenges.FirstOrDefault(x => x.AggregateId == aggregateId);
        }

        public void StoreExample(ExampleModel exampleModel)
        {
            _acceptedChallenges.Add(exampleModel);
        }
    }
}