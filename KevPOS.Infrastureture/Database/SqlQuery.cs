using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using KevPOS.Logging.Infrastructure;

namespace KevPOS.InfrastructureByKevin.Database
{
    /// <summary>
    ///     Please use me for database persistence/loading!
    /// </summary>
    public class SqlQuery : IDisposable
    {
        #region Members

        private SqlConnection DbConnection { get; set; }
        private SqlCommand DbCommand { get; set; }
        private string SqlCommand { get; set; }
        private IList<SqlParameter> Parameters { get; set; }
        public int Timeout { get; set; }

        #endregion

        #region Private Constructors

        private SqlQuery(DataBase database)
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

        private SqlQuery(string connString)
        {
            DbConnection = new SqlConnection(connString);
        }

        #endregion

        #region Constructors

        public SqlQuery(DataBase database, string sqlCommand)
            : this(database)
        {
            SqlCommand = sqlCommand;
            Parameters = new List<SqlParameter>();
        }

        public SqlQuery(DataBase database, string sqlCommand, SqlParameter parameter)
            : this(database)
        {
            SqlCommand = sqlCommand;
            Parameters = new List<SqlParameter> { parameter };
        }

        public SqlQuery(DataBase database, string sqlCommand, IList<SqlParameter> parameters)
            : this(database)
        {
            SqlCommand = sqlCommand;
            Parameters = parameters;
        }

        public SqlQuery(string connection, string sqlCommand)
            : this(connection)
        {
            SqlCommand = sqlCommand;
            Parameters = new List<SqlParameter>();
        }

        public SqlQuery(string connection, string sqlCommand, SqlParameter parameter)
            : this(connection)
        {
            SqlCommand = sqlCommand;
            Parameters = new List<SqlParameter> { parameter };
        }

        public SqlQuery(string connection, string sqlCommand, IList<SqlParameter> parameters)
            : this(connection)
        {
            SqlCommand = sqlCommand;
            Parameters = parameters;
        }

        public SqlQuery(DataBase database, string sqlCommand, IList<SqlParameter> parameters,
            SqlParameter parameter)
            : this(database)
        {
            SqlCommand = sqlCommand;
            Parameters = parameters;
            Parameters.Add(parameter);
        }

        #endregion

        #region Public Methods

        public SqlDataReader GetDataReader()
        {
            SqlDataReader dataReader;
            DbCommand = new SqlCommand(SqlCommand, DbConnection) {CommandType = CommandType.Text};

            if (Parameters.Count > 0)
            {
                DbCommand.Parameters.AddRange(Parameters.ToArray());
            }

            try
            {
                DbCommand.CommandTimeout = Timeout;
                DbCommand.Connection.Open();
                dataReader = DbCommand.ExecuteReader();
            }
            catch (Exception exception)
            {
                Dispose();
                Log4NetLogger.LogEntry(GetType(), "GetDataReader", "GetDataReaderError", LoggerLevel.Error, exception);
                throw;
            }

            return dataReader;
        }

        public object GetScalar()
        {
            object value = null;

            DbCommand = new SqlCommand(SqlCommand, DbConnection) { CommandType = CommandType.Text };

            if (Parameters.Count > 0)
            {
                DbCommand.Parameters.AddRange(Parameters.ToArray());
            }

            try
            {
                DbCommand.CommandTimeout = Timeout;
                DbCommand.Connection.Open();
                value = DbCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                Log4NetLogger.LogEntry(GetType(), "GetScalar", "GetScalarError", LoggerLevel.Error, exception);
            }
            finally
            {
                Dispose();
            }
            return value;
        }

        public void Execute()
        {
            DbCommand = new SqlCommand(SqlCommand, DbConnection) { CommandType = CommandType.Text };

            if (Parameters.Count > 0)
            {
                DbCommand.Parameters.AddRange(Parameters.ToArray());
            }

            try
            {
                DbCommand.CommandTimeout = Timeout;
                DbCommand.Connection.Open();
                DbCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Log4NetLogger.LogEntry(GetType(), "Execute", "ExecuteError", LoggerLevel.Error, exception);
            }
            finally
            {
                Dispose();
            }
        }

        public DataTable GetDataTable()
        {
            var dtResults = new DataTable();
            DbCommand = new SqlCommand(SqlCommand, DbConnection) { CommandType = CommandType.Text };

            if (Parameters.Count > 0)
            {
                DbCommand.Parameters.AddRange(Parameters.ToArray());
            }

            try
            {
                DbCommand.CommandTimeout = Timeout;
                DbCommand.Connection.Open();
                var dataAdapter = new SqlDataAdapter(DbCommand);
                dataAdapter.Fill(dtResults);
            }
            catch (Exception exception)
            {
                Log4NetLogger.LogEntry(GetType(), "GetDataTable", "GetDataTableError", LoggerLevel.Error, exception);
            }
            finally
            {
                Dispose();
            }

            return dtResults;
        }

        //NOT CURRENTLY USED....
        //public DataSet GetDataSet(IEnumerable<string> tableNames)
        //{
        //    var dataSet = new DataSet();
        //    DbCommand = new SqlCommand(SqlCommand, DbConnection) { CommandType = CommandType.Text };

        //    if (Parameters.Count > 0)
        //    {
        //        DbCommand.Parameters.AddRange(Parameters.ToArray());
        //    }

        //    try
        //    {
        //        DbCommand.CommandTimeout = Timeout;
        //        DbCommand.Connection.Open();
        //        var dataAdapter = new SqlDataAdapter(DbCommand);

        //        foreach (string tableName in tableNames)
        //        {
        //            dataSet.Tables.Add(tableName);
        //            dataAdapter.Fill(dataSet, tableName);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Dispose();
        //        Log4NetLogger.LogEntry(GetType(), "GetDataSet", "GetDataSetError", LoggerLevel.Error, exception);
        //        throw;
        //    }

        //    return dataSet;
        //}

        #endregion

        #region IDisposable

        public void Dispose()
        {
            DbCommand.Dispose();
            if (DbConnection.State == ConnectionState.Open)
            {
                DbConnection.Close();
            }
            DbConnection.Dispose();
        }

        #endregion
    }
}