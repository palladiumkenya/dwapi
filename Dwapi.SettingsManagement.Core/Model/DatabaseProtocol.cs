using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class DatabaseProtocol : DbProtocol
    {
        public Guid EmrSystemId { get; set; }
        public ICollection<Extract> Extracts { get; set; }=new List<Extract>();

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

        public DatabaseProtocol(DatabaseType databaseType, string databaseName) : base(databaseType, databaseName)
        {
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
