using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class EmrSystemRepository: BaseRepository<EmrSystem,Guid>, IEmrSystemRepository
    {
        private readonly ISourceReader _reader;
        public EmrSystemRepository(SettingsContext context,ISourceReader reader) : base(context)
        {
            _reader = reader;
        }

        public override IEnumerable<EmrSystem> GetAll()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.DatabaseProtocols)
                .Include(e => e.Extracts)
                .Include(r => r.RestProtocols)
                .ThenInclude(p => p.Resources);
        }

        public int Count()
        {
            return DbSet.Select(x => x.Id).Count();
        }

        public EmrSystem GetDefault()
        {
            var emr = DbSet.AsNoTracking()
                .Include(x => x.DatabaseProtocols)
                .Include(r => r.RestProtocols)
                .ThenInclude(p => p.Resources)
                .Include(e=>e.Extracts)
                .FirstOrDefault(x=>x.IsDefault);
            if (emr != null)
            {
                var extracts = emr.Extracts.OrderBy(c => c.Rank).ToList();
                emr.Extracts = extracts;
                return emr;
            }

            return null;
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
            Create(entity);
        }
        
        public string refreshEMRETL(DatabaseProtocol protocol)
        {
            
            // var sql = $@"CALL openmrs.sp_scheduled_updates()";
            // Context.Database.GetDbConnection().Execute(sql);

            _reader.RefreshEtlTtables(protocol);
            return "status:200";
           
        }
    }
}
