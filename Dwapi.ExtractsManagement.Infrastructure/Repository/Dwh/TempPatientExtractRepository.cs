using System;
using System.Collections.Generic;
using System.Data;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
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
            using (var con=GetConnection())
            {
                if(con.State!=ConnectionState.Open)
                    con.Open();

                con.BulkInsert(extracts);
            }
        }
    }
}