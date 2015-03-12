using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using KevPOS.InfrastructureByKevin.Database;
using KevPOS.TypeExtensions.Infrastructure;

namespace KevPOS.EventStore
{
    public class SpEventDatabase : IEventDatabase
    {
        public async Task<IEnumerable<EventData>> FetchAsync(long aggregateId)
        {
            return  await Task.Run(() => Fetch(aggregateId));
        }

        public async Task StoreAsync(EventData eventData)
        {
            await Task.Run(() => Store(eventData));
        }

        public void Store(EventData eventData)
        {
            using (
                var storedProcedure = new StoredProcedure(DataBase.Default, "sp_InsertEvent",
                    SetUpEventDataParameters(eventData)))
            {
                storedProcedure.Execute();
            }
        }

        public IEnumerable<EventData> Fetch(long id)
        {
            var events = new List<EventData>();
            using (var storedProcedure = new StoredProcedure(DataBase.Default, "sp_SelectEventsByAggregateId",
                new SqlParameter("@AggregateId", id)))
            {
                SqlDataReader reader = storedProcedure.GetDataReader();

                while (reader.Read())
                {
                    events.Add(SetUpEventData(reader));
                }
            }

            return events;
        }

        private EventData SetUpEventData(SqlDataReader reader)
        {
            var evetData = new EventData(reader.SafeValue("AggregateId", new long()), reader.SafeValue("Data", ""),
                reader.SafeValue("Type", ""),
                reader.SafeValue("Created", new DateTime()), reader.SafeValue("Version", 0));

            return evetData;
        }

        private List<SqlParameter> SetUpEventDataParameters(EventData eventData)
        {
            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@AggregateId", eventData.AggregateId),
                new SqlParameter("@Type", eventData.Type),
                new SqlParameter("@Data", eventData.Data),
                new SqlParameter("@Created", eventData.Created),
                new SqlParameter("@Version", eventData.Version),
            };

            return sqlParameters;
        }
    }
}