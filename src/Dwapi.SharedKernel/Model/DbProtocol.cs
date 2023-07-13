using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped] public string DatabaseTypeName => $"{DatabaseType}";

        [NotMapped] public string DiffSqlCheck => GetSql();
        [NotMapped] public bool DiffSupport => !string.IsNullOrWhiteSpace(GetSql());

        [NotMapped] public bool SupportsDifferential => DiffSupport;
        
        public Guid EmrSystemId { get; set; }


        public DbProtocol()
        {
        }
        public DbProtocol(DatabaseType databaseType, string host, string username, string password, string databaseName, Guid emrSystemId)
        {
            DatabaseType = databaseType;
            Host = host;
            Username = username;
            Password = password;
            DatabaseName = databaseName;
            EmrSystemId = emrSystemId;
        }
        public DbProtocol(DatabaseType databaseType,  string databaseName)
        {
            DatabaseType = databaseType;
            DatabaseName = databaseName;
        }

        public void UpdateTo(DbProtocol emrSystem)
        {
            DatabaseType = emrSystem.DatabaseType;
            Host = emrSystem.Host;
            Username = emrSystem.Username;
            Password = emrSystem.Password;
            DatabaseName = emrSystem.DatabaseName;
            EmrSystemId = emrSystem.EmrSystemId;

        }

        public string GetConnectionString()
        {
            string connectionString = string.Empty;

            if (DatabaseType==DatabaseType.Sqlite)
            {
                connectionString = $@"Data Source={DatabaseName.Replace("Data Source=","")};";
            }
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

        public void AddConnectionTimeout()
        {
            if (AdvancedProperties.Contains("Connection Timeout=0"))
                return;
            AdvancedProperties = ";Connection Timeout=0";
        }

        private string GetSql()
        {
            if (Id != new Guid("a6221aa4-0e85-11e8-ba89-0ed5f89f718b"))
                return string.Empty;

            return @"
                     select date_last_modified from kenyaemr_etl.etl_patient_demographics 
                     limit 1
                     ";
        }

        public override string ToString()
        {
            return $"{GetConnectionString()})";
        }
    }
}
