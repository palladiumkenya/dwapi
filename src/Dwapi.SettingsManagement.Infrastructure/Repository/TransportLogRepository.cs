using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class TransportLogRepository : BaseRepository<TransportLog, Guid>, ITransportLogRepository
    {
        public TransportLogRepository(SettingsContext context) : base(context)
        {
        }

        public void Clear(string docket)
        {
            var sql = $@"DELETE FROM {nameof(TransportLog)}s WHERE {nameof(TransportLog.Docket)}='{docket}'";
            Context.Database
                .ExecuteSqlCommand(sql);
        }

        public void CreateLatest(TransportLog transportLog)
        {
            if (transportLog.IsManifest)
            {
                Clear(transportLog.Docket);
                Context.Set<TransportLog>().Add(transportLog);
                Context.SaveChanges();
                return;
            }

            var log = Context.Set<TransportLog>().FirstOrDefault(x => x.Extract == transportLog.Extract);

            if (null == log)
            {
                var manifest = Context.Set<TransportLog>().FirstOrDefault(x => x.Extract == "Manifest");
                if (null != manifest)
                    transportLog.SetManifest(manifest);

                Context.Set<TransportLog>().Add(transportLog);
            }
            else
            {
                log.SetLatest(transportLog);
                Context.Update(log);
            }

            Context.SaveChanges();
        }

        public TransportLog GetManifest()
        {
            return Context.Set<TransportLog>().FirstOrDefault(x => x.IsManifest);
        }

        public TransportLog GetMainExtract()
        {
            return Context.Set<TransportLog>().FirstOrDefault(x => x.IsMainExtract);
        }
    }
}
