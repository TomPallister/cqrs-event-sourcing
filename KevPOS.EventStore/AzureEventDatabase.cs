using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using FirstOneTo.Azure.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace KevPOS.EventStore
{
    public class AzureEventDatabase : IEventDatabase
    {
        private readonly CloudTable _cloudTable;

        public AzureEventDatabase(CloudStorageAccount storageAccount, string tableReference)
        {
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
            _cloudTable = cloudTableClient.GetTableReference(tableReference);
            _cloudTable.CreateIfNotExists();
        }

        public async Task StoreAsync(EventData eventData)
        {
            var eventEntity = new EventEntity(eventData.AggregateId, eventData.Data, eventData.Type, eventData.Created,
                eventData.Version);
            TableOperation insertOperation = TableOperation.Insert(eventEntity);
            await _cloudTable.ExecuteAsync(insertOperation);
        }

        public async Task<IEnumerable<EventData>> FetchAsync(long aggregateId)
        {
            return await Task.Run(() => Fetch(aggregateId));
        }

        public void Store(EventData eventData)
        {
            var eventEntity = new EventEntity(eventData.AggregateId, eventData.Data, eventData.Type, eventData.Created,
                eventData.Version);
            TableOperation insertOperation = TableOperation.Insert(eventEntity);
            _cloudTable.Execute(insertOperation);
        }

        public IEnumerable<EventData> Fetch(long aggregateId)
        {
            var eventDatas = new List<EventData>();
            TableQuery<EventEntity> query =
                new TableQuery<EventEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
                    QueryComparisons.Equal, aggregateId.ToString(CultureInfo.InvariantCulture)));

            foreach (EventEntity entity in _cloudTable.ExecuteQuery(query))
            {
                var eventData = new EventData(entity.AggregateId, entity.Data, entity.Type, entity.Created,
                    entity.Version);

                eventDatas.Add(eventData);
            }
            return eventDatas;
        }
    }
}