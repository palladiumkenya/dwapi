using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Utility;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class DocketRepository : BaseRepository<Docket, Guid>, IDocketRepository
    {
        public DocketRepository(SettingsContext context) : base(context)
        {

        }

        public Docket GetByCode(string code)
        {
            return DbSet.AsNoTracking()
                .Include(e => e.Extracts)
                .ThenInclude(d => d.Destinations)
                .FirstOrDefault(x => x.Code.IsSameAs(code));
        }
    }
}