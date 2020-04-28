using Dwapi.SharedKernel.Enum;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class ConnectionStrings
    {
        public DatabaseProvider Provider { get; set; }
        public string DwapiConnection { get; set; }
        
        public ConnectionStrings()
        {
        }

        public ConnectionStrings(string dwapiConnection, DatabaseProvider provider=0)
        {
            Provider = provider;
            DwapiConnection = dwapiConnection;
        }
    }
}
