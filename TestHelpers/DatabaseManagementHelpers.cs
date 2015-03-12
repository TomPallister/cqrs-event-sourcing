using System.Data.SqlClient;

namespace TestHelpers
{
    public static class DatabaseManagementHelpers
    {
        public static void TearDownDatabase(string connectionString)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand("delete from eventstore.Event", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }

                using (var sqlCommand = new SqlCommand("delete from readmodel.category", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }

                using (var sqlCommand = new SqlCommand("delete from readmodel.product", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }

                using (var sqlCommand = new SqlCommand("delete from readmodel.variant", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }

                using (var sqlCommand = new SqlCommand("delete from readmodel.attribute", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }

                using (var sqlCommand = new SqlCommand("delete from eventstore.Command", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}