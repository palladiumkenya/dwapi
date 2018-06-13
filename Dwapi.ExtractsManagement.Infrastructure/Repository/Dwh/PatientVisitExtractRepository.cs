using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class PatientVisitExtractRepository : BaseRepository<PatientVisitExtract, Guid>, IPatientVisitExtractRepository
    {
        public PatientVisitExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<PatientVisitExtract> extracts)
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