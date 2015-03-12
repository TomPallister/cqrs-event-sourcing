using System;
using System.Globalization;
using Microsoft.WindowsAzure.Storage.Table;

namespace FirstOneTo.Azure.Entities
{
    public class ExampleEntity : TableEntity
    {
        public ExampleEntity()
        {
        }

        public ExampleEntity(long aggregateId, string description)
        {
            PartitionKey = aggregateId.ToString(CultureInfo.InvariantCulture);
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            AggregateId = aggregateId;
            Description = description;
        }

        public long AggregateId { get; set; }
        public string Description { get; set; }
    }
}