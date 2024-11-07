using System;
using System.Data;
using System.Data.Common;
using Dapper;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Serilog;

namespace Dwapi.SettingsManagement.Infrastructure
{
    public class DatabaseManager : IDatabaseManager
    {
        private string _connectionError;

        public string ConnectionError
        {
            get { return _connectionError; }
        }

        public IDbConnection GetConnection(DatabaseProtocol databaseProtocol)
        {
            var connectionString = databaseProtocol.GetConnectionString();

            if (databaseProtocol.DatabaseType == DatabaseType.Sqlite)
                return new SqliteConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MicrosoftSQL)
                return new SqlConnection(connectionString);

            if (databaseProtocol.DatabaseType == DatabaseType.MySQL)
                return new MySqlConnection(connectionString);

            return null;
        }

        public bool VerifyConnection(DatabaseProtocol databaseProtocol)
        {
            var connection = GetConnection(databaseProtocol);
            if (null == connection)
                throw new ArgumentException("connection not initialized");

            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
                catch (Exception e)
                {
                    if (e is DbException)
                    {
                        _connectionError = e.Message;
                    }
                    Log.Debug($"{e}");
                    throw;
                }
            }

            return false;
        }

        public bool VerifyQuery(Extract extract, DatabaseProtocol databaseProtocol)
        {
            var connection = GetConnection(databaseProtocol);
            if (null == connection)
                throw new ArgumentException("connection not initialized");

            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                    connection.Execute(extract.ExtractSql);
                    return true;
                }
                catch (Exception e)
                {
                    if (e is DbException)
                    {
                        _connectionError = e.Message;
                    }
                    Log.Debug($"{e}");
                    throw;
                }
            }

            return false;
        }
    }
}
