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

        public EmrSystem GetDefault()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.DatabaseProtocols)
                .Include(r => r.RestProtocols)
                .Include(e=>e.Extracts)
                .FirstOrDefault(x=>x.IsDefault);
        }

        public EmrSystem GetMiddleware()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.DatabaseProtocols)
                .Include(r => r.RestProtocols)
                .Include(e => e.Extracts)
                .FirstOrDefault(x => x.IsMiddleware);
        }

        public override void CreateOrUpdate(EmrSystem entity)
        {
            if (null == entity)
                return;

            var exists = DbSet.AsNoTracking().FirstOrDefault(x => Equals(x.Id, entity.Id));
            if (null != exists)
            {
                Update(entity);
                return;
            }
            entity.Extracts.Add(Extract.CreatePsmart(entity.Id));
            Create(entity);
        }
    }
}