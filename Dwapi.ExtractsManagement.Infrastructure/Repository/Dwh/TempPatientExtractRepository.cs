using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientExtractRepository:BaseRepository<TempPatientExtract,Guid> ,ITempPatientExtractRepository
    {
        public TempPatientExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public void BatchInsert(IEnumerable<TempPatientExtract> extracts)
        {
            try
            {
                GetConnection().BulkInsert(extracts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}