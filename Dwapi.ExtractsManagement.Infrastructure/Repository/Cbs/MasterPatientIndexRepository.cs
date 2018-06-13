using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
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
                var connection = GetConnection();
                connection.BulkInsert(extracts);
                CloseConnection(connection);
                return true;
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

        public void UpdateSendStatus(List<SentItem> sentItems)
        {
            var mpi = GetAll(x => sentItems.Select(i => i.Id).Contains(x.Id))
                .Select(x =>
                {
                    var sentItem = sentItems.First(s => s.Id == x.Id);
                    x.Status = $"{sentItem.Status}";
                    x.StatusDate = sentItem.StatusDate;
                    return x;
                });

            var cn = GetConnection();
            cn.BulkUpdate(mpi);
            CloseConnection(cn);
        }
    }
}