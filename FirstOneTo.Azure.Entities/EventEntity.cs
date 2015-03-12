using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace FirstOneTo.Azure.Entities
{
    public class EventEntity : TableEntity
    {
        public EventEntity()
        {
            
        }

        public EventEntity(long aggregateId, string data, string type, DateTime created, int verson)
        {
            PartitionKey = aggregateId.ToString(CultureInfo.InvariantCulture);
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            AggregateId = aggregateId;
            Data = data;
            Type = type;
            Created = created;
            Version = verson;
        }

        public long AggregateId { get; set; }
        public string Data { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public int Version { get; set; }
    }
}
