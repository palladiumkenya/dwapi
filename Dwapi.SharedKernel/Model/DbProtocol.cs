using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Model
{
    public class DbProtocol : Entity<Guid>
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

        public DbProtocol()
        {
        }
        public DbProtocol(DatabaseType databaseType, string host, string username, string password, string databaseName)
        {
            DatabaseType = databaseType;
            Host = host;
            Username = username;
            Password = password;
            DatabaseName = databaseName;
        }
      
        public void UpdateTo(DbProtocol emrSystem)
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