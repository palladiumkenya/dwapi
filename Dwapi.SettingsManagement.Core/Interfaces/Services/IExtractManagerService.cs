using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Interfaces.Services
{
    public interface IExtractManagerService
    {
        IEnumerable<Extract> GetAllByEmr(Guid emrId,string docketId);
        void Save(Extract extract);
        bool Verfiy(Extract extract, DatabaseProtocol databaseProtocol);
        string GetDatabaseError();
    }
}