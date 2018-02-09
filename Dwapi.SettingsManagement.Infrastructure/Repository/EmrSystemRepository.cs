using System;
using System.Collections.Generic;
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

        public override IEnumerable<EmrSystem> GetAll()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.DatabaseProtocols)
                .Include(r=>r.RestProtocols);
        }
    }
}