using System.Data;
using System.Data.SqlClient;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;

namespace Dwapi.SettingsManagement.Infrastructure
{
    public class AppDatabaseManager : IAppDatabaseManager
    {
        public bool VerfiyServer(AppDatabase appDatabase)
        {
            bool isConnected = false;

            if (appDatabase.Provider == DatabaseProvider.Other)
            {
                using (var cn = new SqliteConnection(appDatabase.Database))
                {
                    cn.Open();
                    isConnected = cn.State == ConnectionState.Open;
                }
            }

            if (appDatabase.Provider == DatabaseProvider.MsSql)
            {
                var sb = new SqlConnectionStringBuilder();
                sb.DataSource = appDatabase.Server;
                sb.UserID = appDatabase.User;
                sb.Password = appDatabase.Password;
                sb.InitialCatalog = "master";
                sb.MultipleActiveResultSets = true;
                sb.PersistSecurityInfo = true;
                sb.Pooling = true;

                using (var cn = new SqlConnection(sb.ConnectionString))
                {
                    cn.Open();
                    isConnected = cn.State == ConnectionState.Open;
                }
            }

            if (appDatabase.Provider == DatabaseProvider.MySql)
            {
                var sb = new MySqlConnectionStringBuilder();
                sb.Server = appDatabase.Server;
                sb.UserID = appDatabase.User;
                sb.Password = appDatabase.Password;
                sb.Database = "mysql";
                sb.Pooling = true;
                sb.PersistSecurityInfo = true;
                sb.ConvertZeroDateTime = true;
                sb.Port = appDatabase.Port;

                using (var cn = new MySqlConnection(sb.ConnectionString))
                {
                    cn.Open();
                    isConnected = cn.State == ConnectionState.Open;
                }
            }

            return isConnected;
        }

        public bool Verfiy(AppDatabase appDatabase)
        {
            bool isConnected = false;

            if (appDatabase.Provider == DatabaseProvider.Other)
            {
                using (var cn = new SqliteConnection(appDatabase.Database))
                {
                    cn.Open();
                    isConnected = cn.State == ConnectionState.Open;
                }
            }

            if (appDatabase.Provider == DatabaseProvider.MsSql)
            {
                var sb = new SqlConnectionStringBuilder();
                sb.DataSource = appDatabase.Server;
                sb.UserID = appDatabase.User;
                sb.Password = appDatabase.Password;
                sb.InitialCatalog = appDatabase.Database;
                sb.MultipleActiveResultSets = true;
                sb.PersistSecurityInfo = true;
                sb.Pooling = true;

                using (var cn = new SqlConnection(sb.ConnectionString))
                {
                    cn.Open();
                    isConnected = cn.State == ConnectionState.Open;
                }
            }

            if (appDatabase.Provider == DatabaseProvider.MySql)
            {
                var sb = new MySqlConnectionStringBuilder();
                sb.Server = appDatabase.Server;
                sb.UserID = appDatabase.User;
                sb.Password = appDatabase.Password;
                sb.Database = appDatabase.Database;
                sb.PersistSecurityInfo = true;
                sb.Pooling = true;
                sb.PersistSecurityInfo = true;
                sb.ConvertZeroDateTime = true;
                sb.Port = appDatabase.Port;

                using (var cn = new MySqlConnection(sb.ConnectionString))
                {
                    cn.Open();
                    isConnected = cn.State == ConnectionState.Open;
                }
            }

            return isConnected;
        }

        public bool Verfiy(string connectionString, DatabaseProvider provider)
        {
            return Verfiy(ReadConnection(connectionString,provider));
        }

        public string BuildConncetion(AppDatabase appDatabase)
        {
            if (appDatabase.Provider == DatabaseProvider.Other)
            {
                return appDatabase.Database;
            }

            if (appDatabase.Provider == DatabaseProvider.MsSql)
            {
                var sb = new SqlConnectionStringBuilder();
                sb.DataSource = appDatabase.Server;
                sb.UserID = appDatabase.User;
                sb.Password = appDatabase.Password;
                sb.InitialCatalog = appDatabase.Database;
                sb.MultipleActiveResultSets = true;
                sb.Pooling = true;
                sb.PersistSecurityInfo = true;
                return sb.ConnectionString;
            }

            if (appDatabase.Provider == DatabaseProvider.MySql)
            {
                var sb = new MySqlConnectionStringBuilder();
                sb.Server = appDatabase.Server;
                sb.UserID = appDatabase.User;
                sb.Password = appDatabase.Password;
                sb.Database = appDatabase.Database;
                sb.Pooling = true;
                sb.PersistSecurityInfo = true;
                sb.ConvertZeroDateTime = true;
                sb.Port = appDatabase.Port;
                return sb.ConnectionString;
            }

            return null;
        }

        public AppDatabase ReadConnection(string connectionString, DatabaseProvider provider)
        {
            if (provider == DatabaseProvider.Other)
            {
                var sb = new SqliteConnectionStringBuilder(connectionString);
                return new AppDatabase(sb.ConnectionString, provider);
            }

            if (provider == DatabaseProvider.MsSql)
            {
                var sb = new SqlConnectionStringBuilder(connectionString);
                return new AppDatabase(sb.DataSource, sb.InitialCatalog, sb.UserID, sb.Password, provider,1433);
            }

            if (provider == DatabaseProvider.MySql)
            {
                var sb = new MySqlConnectionStringBuilder(connectionString);
                return new AppDatabase(sb.Server, sb.Database, sb.UserID, sb.Password, provider,sb.Port);
            }

            return null;
        }
    }
}
