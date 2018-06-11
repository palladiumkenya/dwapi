using Dwapi.SharedKernel.Enum;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class ConnectionStrings
    {
        public DatabaseProvider Provider { get; set; }
        public string SpotConnection { get; set; }
        
        public ConnectionStrings()
        {
        }

        public ConnectionStrings(string spotConnection, DatabaseProvider provider=0)
        {
            Provider = provider;
            SpotConnection = spotConnection;
        }
    }
}
