using System;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class EmrSystemRepository: BaseRepository<EmrSystem,Guid>, IEmrSystemRepository
    {
        public EmrSystemRepository(DbContext context) : base(context)
        {
        }
    }
}