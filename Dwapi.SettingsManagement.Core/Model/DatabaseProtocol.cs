using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class DatabaseProtocol : DbProtocol
    {
        public Guid EmrSystemId { get; set; }

        public DatabaseProtocol()
        {
        }

        public DatabaseProtocol(DatabaseType databaseType, string host, string username, string password, string databaseName) : base(databaseType, host, username, password, databaseName)
        {
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
    }
}