using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Prep.Base
{
    public abstract class TempPrepExtractErrorSummaryRepository<T> : ITempPrepExtractErrorSummaryRepository<T> where T: TempPrepExtractErrorSummary
    {
        internal ExtractsContext Context;
        internal DbSet<T> DbSet;

        public TempPrepExtractErrorSummaryRepository(ExtractsContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IEnumerable<T> GetByExtract(string extract)
        {
            return DbSet.Where(x => x.Extract.ToLower() == extract.ToLower());
        }
    }
}
