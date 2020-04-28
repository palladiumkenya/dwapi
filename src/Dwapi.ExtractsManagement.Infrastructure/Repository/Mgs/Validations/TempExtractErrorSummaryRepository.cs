using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mgs.Validations
{
    public abstract class TempMetricExtractErrorSummaryRepository<T> : ITempMetricExtractErrorSummaryRepository<T> where T: TempMetricExtractErrorSummary
    {
        internal ExtractsContext Context;
        internal DbSet<T> DbSet;

        public TempMetricExtractErrorSummaryRepository(ExtractsContext context)
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
