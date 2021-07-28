using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Serilog;

namespace Dwapi.ExtractsManagement.Infrastructure.Reader
{

    public abstract class SourceReader : ISourceReader
    {
        public virtual IDbConnection Connection { get; private set; }
        public virtual int Find(DbProtocol protocol, DbExtract extract)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDataReader> ExecuteReader(DbProtocol protocol, DbExtract extract)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            if (null == extract)
                throw new Exception("Extract settings not configured");

            if (sourceConnection.State != ConnectionState.Open)
                sourceConnection.Open();

            Connection = sourceConnection;
            var commandDefinition = new CommandDefinition(extract.ExtractSql, null, null, 3600);

            if (sourceConnection is SqliteConnection)
                return Task.FromResult<IDataReader>(sourceConnection.ExecuteReader(commandDefinition));

            return sourceConnection.ExecuteReaderAsync(commandDefinition, CommandBehavior.CloseConnection);
        }

        public IDataReader ExecuteReaderSync(DbProtocol protocol, DbExtract extract)
        {
            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            if (null == extract)
                throw new Exception("Extract settings not configured");

            if (sourceConnection.State != ConnectionState.Open)
                sourceConnection.Open();

            Connection = sourceConnection;
            var commandDefinition = new CommandDefinition(extract.ExtractSql, null, null, 3600);

            if (sourceConnection is SqliteConnection)
                return sourceConnection.ExecuteReader(commandDefinition);

            return sourceConnection.ExecuteReader(commandDefinition, CommandBehavior.CloseConnection);
        }

        public bool CheckDiffSupport(DbProtocol protocol)
        {
            if (!protocol.DiffSupport)
                return false;

            var sourceConnection = GetConnection(protocol);
            if (null == sourceConnection)
                throw new Exception("Data connection not initialized");

            using (sourceConnection)
            {
                if(sourceConnection.State!=ConnectionState.Open)
                    sourceConnection.Open();
                var commandDefinition = new CommandDefinition(protocol.DiffSqlCheck, null, null, 0);

                try
                {
                    if (sourceConnection is SqliteConnection)
                    {
                        using (var diffReader =sourceConnection.ExecuteReader(commandDefinition))
                        {
                            diffReader.Read();
                        }
                    }
                    else
                    {
                        using (var diffReader = sourceConnection.ExecuteReader(commandDefinition, CommandBehavior.CloseConnection))
                        {
                            diffReader.Read();
                        }
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Log.Warning("Missing differential upload feature in EMR");
                }
            }
            return false;
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
