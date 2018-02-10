using System.Data;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Interfaces
{
    public interface IDatabaseManager
    {
        string ConnectionError { get; }
        IDbConnection GetConnection(DatabaseProtocol databaseProtocol);
        bool VerifyConnection(DatabaseProtocol databaseProtocol);
    }
}