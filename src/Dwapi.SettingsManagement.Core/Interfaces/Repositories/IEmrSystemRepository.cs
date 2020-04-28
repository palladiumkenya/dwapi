using System;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Interfaces.Repositories
{
    public interface IEmrSystemRepository : IRepository<EmrSystem,Guid>
    {
        int Count();
        EmrSystem GetDefault();
        EmrSystem GetMiddleware();
    }
}