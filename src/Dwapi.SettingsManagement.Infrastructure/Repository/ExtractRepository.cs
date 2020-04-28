using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class ExtractRepository:BaseRepository<Extract,Guid>, IExtractRepository
    {
        public ExtractRepository(SettingsContext context) : base(context)
        {
        }

        public IEnumerable<Extract> GetAllByEmr(Guid emrSystemId, string docketId)
        {
            return DbSet
                .Where(x => x.EmrSystemId == emrSystemId &&
                            x.DocketId.IsSameAs(docketId));
        }
    }
}