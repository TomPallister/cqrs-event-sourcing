using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using KevPOS.InfrastructureByKevin.Database;
using KevPOS.TypeExtensions.Infrastructure;

namespace KevPOS.CommandStore
{
    public class SpCommandDatabase : ICommandDatabase
    {
        public void Store(CommandData eventData)
        {
            using (
                var storedProcedure = new StoredProcedure(DataBase.Default, "sp_InsertCommand",
                    SetUpCommandDataParameters(eventData)))
            {
                 storedProcedure.Execute();
            }
        }

        private CommandData SetUpCommandData(SqlDataReader reader)
        {
            var evetData = new CommandData(reader.SafeValue("AggregateId", new long()), reader.SafeValue("Data", ""),
                reader.SafeValue("Type", ""),
                reader.SafeValue("Created", new DateTime()));

            return evetData;
        }

        private List<SqlParameter> SetUpCommandDataParameters(CommandData commandData)
        {
            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@AggregateId", commandData.AggregateId),
                new SqlParameter("@Type", commandData.Type),
                new SqlParameter("@Data", commandData.Data),
                new SqlParameter("@Created", commandData.Created),
                new SqlParameter("@Version", commandData.Version),
            };

            return sqlParameters;
        }
    }
}