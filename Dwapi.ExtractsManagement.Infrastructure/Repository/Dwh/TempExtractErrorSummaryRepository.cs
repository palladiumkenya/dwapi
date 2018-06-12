using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public abstract class TempExtractErrorSummaryRepository<T> : ITempExtractErrorSummaryRepository<T> where T: TempExtractErrorSummary
    {
        internal ExtractsContext Context;
        internal DbSet<T> DbSet;

        public TempExtractErrorSummaryRepository(ExtractsContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IPagedList<T> GetAll(int? page, int? pageSize, string search = "")
        {
            page = page == 0 ? 1 : page;
            pageSize = pageSize == 0 ? 100 : pageSize;
            pageSize = pageSize == -1 ? GetAll().Count() : pageSize;

            var tempExtracts = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                tempExtracts = tempExtracts.Where(n =>
                    n.PatientID.ToLower().Contains(search.ToLower())
                );
            }


            var pagedlist = tempExtracts
                .OrderBy(x => x.PatientID).ThenBy(x=>x.Summary)
                .ToPagedList(page.Value, pageSize.Value);

            return pagedlist;
        }

        public IEnumerable<T> GetByExtract(string extract)
        {
            return DbSet.Where(x => x.Extract.ToLower() == extract.ToLower());
        }
    }
}
