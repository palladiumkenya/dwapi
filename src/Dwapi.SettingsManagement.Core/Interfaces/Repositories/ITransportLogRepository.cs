using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Interfaces.Repositories
{
    public interface ITransportLogRepository : IRepository<TransportLog,Guid>
    {
        void Clear(string docket);
        void CreateLatest(TransportLog transportLog);

        TransportLog GetManifest();
        TransportLog GetMainExtract();
    }
}
