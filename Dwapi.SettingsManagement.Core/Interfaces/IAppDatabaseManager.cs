using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;

namespace Dwapi.SettingsManagement.Core.Interfaces
{
  public interface IAppDatabaseManager
  {
    bool VerfiyServer(AppDatabase appDatabase);
    bool Verfiy(AppDatabase appDatabase);
    bool Verfiy(string connectionString, DatabaseProvider provider);
    string BuildConncetion(AppDatabase appDatabase);
    AppDatabase ReadConnection(string connectionString,DatabaseProvider provider);

  }
}
