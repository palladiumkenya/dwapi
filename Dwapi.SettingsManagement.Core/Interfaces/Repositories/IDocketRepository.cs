using System;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Interfaces.Repositories
{
    public interface IDocketRepository : IRepository<Docket,Guid>
    {
        Docket GetByCode(string code);
    }
}