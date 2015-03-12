using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KevPOS.IdGenerator
{
    public class BlockGenerator : IIdBlockGenerator
    {
        private readonly string _connectionString;
        private readonly object _syncLock = new object();
        private long _blockId;
        private readonly string _sqlUpdateStatement;

        public BlockGenerator()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ReadCurrentBlockId();
            _sqlUpdateStatement = "update control.IdBlock set CurrentBlock =";
        }

        public long NextBlock()
        {
            lock (_syncLock)
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (
                        var command =
                            new SqlCommand(string.Format("{0} {1}", _sqlUpdateStatement, ++_blockId),
                                connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            return _blockId;
        }

        public int BlockCapacity()
        {
            return 10;
        }

        private void ReadCurrentBlockId()
        {
            lock (_syncLock)
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("select CurrentBlock from control.IdBlock", connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                _blockId = (long) dataReader["CurrentBlock"];
                            }
                        }
                    }
                }
        }
    }
}