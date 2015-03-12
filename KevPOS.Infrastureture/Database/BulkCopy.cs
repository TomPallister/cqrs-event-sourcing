using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KevPOS.InfrastructureByKevin.Database
{
    public class BulkCopy
    {
        public BulkCopy(DataBase database)
        {
            switch (database)
            {
                case DataBase.Default:
                    DbConnection =
                        new SqlConnection(
                            ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString);
                    break;
            }
        }

        private SqlConnection DbConnection { get; set; }

        public void Upload(DataTable data, string tableName)
        {
            using (DbConnection)
            {
                var bulkCopy = new SqlBulkCopy(DbConnection, SqlBulkCopyOptions.TableLock, null)
                {
                    DestinationTableName = tableName
                };
                DbConnection.Open();

                bulkCopy.WriteToServer(data);
            }
        }
    }
}