using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Serilog;

namespace Dwapi.ExtractsManagement.Infrastructure.Reader.Hts
{
    public class HTSExtractSourceReader : IHTSExtractSourceReader
    {
        public IDbConnection Connection { get; private set; }
        public int Find(DbProtocol protocol, DbExtract extract)
        {
            // TODO: Allow User Variables=True
            throw new NotImplementedException();
        }

        public Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            if (null == extract)
                throw new Exception("Extract settings not configured");

            if (sourceConnection.State != ConnectionState.Open)
                sourceConnection.Open();

            Connection = sourceConnection;

            var commandDefinition = new CommandDefinition(extract.ExtractSql, null, null, 0);

            if(sourceConnection is SqliteConnection)
                return Task.FromResult<IDataReader>(sourceConnection.ExecuteReader(commandDefinition));
            return sourceConnection.ExecuteReaderAsync(commandDefinition, CommandBehavior.CloseConnection);
        }

        public IDbConnection GetConnection(DbProtocol databaseProtocol)
        {
            var connectionString = databaseProtocol.GetConnectionString();
            Log.Debug(new string('+',40));
            Log.Debug(connectionString);
            Log.Debug(new string('+',40));

            if (databaseProtocol.DatabaseType == DatabaseType.Sqlite)
                return new SqliteConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MicrosoftSQL)
                return new System.Data.SqlClient.SqlConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MySQL)
                return new MySqlConnection(connectionString);

            return null;
        }
    }
}
