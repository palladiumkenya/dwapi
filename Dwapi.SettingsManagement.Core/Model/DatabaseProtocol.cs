using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class DatabaseProtocol : Entity<Guid>
    {
        public DatabaseType DatabaseType { get; set; }

        [MaxLength(100)]
        public string Host { get; set; }

        public int? Port { get; set; }

        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string DatabaseName { get; set; }
        [MaxLength(100)]
        public string AdvancedProperties { get; set; }

        public Guid EmrSystemId { get; set; }

        public DatabaseProtocol()
        {
        }
        public DatabaseProtocol(DatabaseType databaseType, string host, string username, string password, string databaseName)
        {
            DatabaseType = databaseType;
            Host = host;
            Username = username;
            Password = password;
            DatabaseName = databaseName;
        }
        public DatabaseProtocol(DatabaseType databaseType, string host, string username, string password, string databaseName, Guid emrSystemId)
        :this(databaseType,host,username,password,databaseName)
        {
            EmrSystemId = emrSystemId;
        }

        public void UpdateTo(DatabaseProtocol emrSystem)
        {
            DatabaseType = emrSystem.DatabaseType;
            Host = emrSystem.Host;
            Username = emrSystem.Username;
            Password = emrSystem.Password;
            DatabaseName = emrSystem.DatabaseName;
        }

        public string GetConnectionString()
        {
            string connectionString = string.Empty;

            if (DatabaseType==DatabaseType.MicrosoftSQL)
            {
                connectionString = $@"Data Source={Host};Initial Catalog={DatabaseName};User ID={Username};Password={Password};";
            }
            if (DatabaseType==DatabaseType.MySQL)
            {
                Port = Port > 0 ? Port : 3306;
                connectionString = $@"Server={Host};Port={Port};Database={DatabaseName};Uid={Username};Pwd={Password};";
            }
          
            connectionString = connectionString.HasToEndsWith(";");

            connectionString = string.IsNullOrWhiteSpace(AdvancedProperties)
                ? $"{connectionString}"
                : $"{connectionString}{AdvancedProperties}";

            return connectionString;
        }

        public override string ToString()
        {
            return $"{GetConnectionString()})";
        }
    }
}