using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class EmrSystemRepository: BaseRepository<EmrSystem,Guid>, IEmrSystemRepository
    {
        public EmrSystemRepository(SettingsContext context) : base(context)
        {
           
        }

        public override IEnumerable<EmrSystem> GetAll()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.DatabaseProtocols)
                .Include(r => r.RestProtocols);
        }

        public int Count()
        {
            return DbSet.Select(x => x.Id).Count();
        }
    }
}