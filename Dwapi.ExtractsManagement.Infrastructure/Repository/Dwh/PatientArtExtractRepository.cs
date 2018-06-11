using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class PatientArtExtractRepository : BaseRepository<PatientArtExtract, Guid>, IPatientArtExtractRepository
    {
        public PatientArtExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<PatientArtExtract> extracts)
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

        public IEnumerable<PatientArtExtract> BatchGet()
        {
            var cn = GetConnectionString();

            using (var connection = new SqlConnection(cn))
            {
                var patientArtExtracts = connection.GetAll<PatientArtExtract>();
                return patientArtExtracts;
            }
        }
    }
}