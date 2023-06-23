using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Interfaces.Services
{
    public interface IAutoloadService
    {
        string refreshEMRETL(DatabaseProtocol protocol);
        // EmrSystem GetMiddleware();
        // IEnumerable<EmrSystem> GetAllEmrs();
        // int GetEmrCount();
        // void SaveEmr(EmrSystem emrSystem);
        // void DeleteEmr(Guid emrId);
        // void SaveProtocol(DatabaseProtocol protocol);
        // void SaveRestProtocol(RestProtocol protocol);
        // void SaveResource(Resource resource);
        // void DeleteProtocol(Guid protocolId);
        // bool VerifyConnection(DatabaseProtocol databaseProtocol);
        // string GetConnectionError();
        // void SetDefault(Guid id);
        // IEnumerable<DatabaseProtocol> GetByEmr(Guid emrId);
    }
}