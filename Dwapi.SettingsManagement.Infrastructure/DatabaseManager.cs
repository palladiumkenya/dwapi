using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using MySql.Data.MySqlClient;
using Serilog;

namespace Dwapi.SettingsManagement.Infrastructure
{
    public class DatabaseManager : IDatabaseManager
    {
        public IDbConnection GetConnection(DatabaseProtocol databaseProtocol)
        {
            var connectionString = databaseProtocol.GetConnectionString();

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
                   Log.Debug($"{e}");
                }
            }

            return false;
        }
    }
}