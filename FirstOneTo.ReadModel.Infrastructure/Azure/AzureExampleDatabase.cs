using System.Globalization;
using System.Linq;
using FirstOneTo.Azure.Entities;
using FirstOneTo.Readmodel.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace FirstOneTo.ReadModel.Infrastructure.Azure
{
    public class AzureExampleDatabase : IExampleDatabase
    {
        private readonly CloudTable _cloudTable;

        public AzureExampleDatabase(CloudStorageAccount cloudStorageAccount, string tableReference)
        {
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            _cloudTable = cloudTableClient.GetTableReference(tableReference);
            _cloudTable.CreateIfNotExists();
        }

        public ExampleModel GetExample(long aggregateId)
        {
            IQueryable<ExampleModel> query = from entity in _cloudTable.CreateQuery<ExampleEntity>()
                where entity.PartitionKey == aggregateId.ToString(CultureInfo.InvariantCulture)
                select new ExampleModel(entity.AggregateId,
                    entity.Description);
            return query.FirstOrDefault();
        }

        public void StoreExample(ExampleModel exampleModel)
        {
            var acceptedChallengeEntity = new ExampleEntity(exampleModel.AggregateId, exampleModel.Description);
            TableOperation insertOperation = TableOperation.Insert(acceptedChallengeEntity);
            _cloudTable.ExecuteAsync(insertOperation);
        }
    }
}