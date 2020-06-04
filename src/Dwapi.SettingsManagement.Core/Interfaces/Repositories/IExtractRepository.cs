using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Interfaces.Repositories
{
    public interface IExtractRepository : IRepository<Extract, Guid>
    {
        IEnumerable<Extract> GetAllRelated(Guid extractId);

        IEnumerable<Extract> GetAllByEmr(Guid emrSystemId,string docketId);
    }
}
