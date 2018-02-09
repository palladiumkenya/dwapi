using System;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Interfaces.Repositories
{
    public interface ICentralRegistryRepository : IRepository<CentralRegistry,Guid>
    {
        CentralRegistry GetDefault();
        void SaveDefault(CentralRegistry centralRegistry);
    }
}