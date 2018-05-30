using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientExtractErrorSummaryRepository : BaseRepository<TempPatientExtractErrorSummary, Guid>, ITempPatientExtractErrorSummaryRepository
    {
        private ExtractsContext Context;
        public TempPatientExtractErrorSummaryRepository(ExtractsContext context) : base(context)
        {
            Context = context;
        }

        public IEnumerable<TempPatientExtractErrorSummary> GetByExtract(string extract)
        {
            return Context.TempPatientExtractErrorSummary.Where(x => x.Extract.ToLower() == extract.ToLower());
        }
    }
}