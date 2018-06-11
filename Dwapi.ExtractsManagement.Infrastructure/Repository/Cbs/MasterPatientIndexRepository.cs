using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs
{
    public class MasterPatientIndexRepository : BaseRepository<MasterPatientIndex, Guid>, IMasterPatientIndexRepository
    {
        public MasterPatientIndexRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<MasterPatientIndex> extracts)
        {
            var cn = GetConnectionString();
            try
            {
                using (var connection = new SqlConnection(cn))
                {
                    connection.BulkInsert(extracts);
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Error(e, "Failed batch insert");
                return false;
            }
        }

        public IEnumerable<MasterPatientIndex> GetView()
        {
            var ctx = Context as ExtractsContext;
            return ctx.MasterPatientIndices.FromSql("select * from vMasterPatientIndicesJaroV3");
        }
    }
}