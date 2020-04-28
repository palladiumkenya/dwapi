using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.Validations.Base
{
    public abstract class TempHTSExtractErrorSummaryRepository<T> : ITempHTSExtractErrorSummaryRepository<T> where T: TempHTSExtractErrorSummary
    {
        internal ExtractsContext Context;
        internal DbSet<T> DbSet;

        public TempHTSExtractErrorSummaryRepository(ExtractsContext context)
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
