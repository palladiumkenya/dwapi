using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Infrastructure.Repository;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh
{
    public class TempPatientPharmacyExtractRepository: BaseRepository<TempPatientPharmacyExtract, Guid>, ITempPatientPharmacyExtractRepository
    {
        public TempPatientPharmacyExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<TempPatientPharmacyExtract> extracts)
        {
            var cn = GetConnectionString();
            try
            {
                if(Context.Database.ProviderName.ToLower().Contains("SqlServer".ToLower()))
                {
                    using (var connection = new SqlConnection(cn))
                    {
                        connection.BulkInsert(extracts);
                        return true;
                    }
                }

                if (Context.Database.ProviderName.ToLower().Contains("MySql".ToLower()))
                {
                    using (var connection = new MySqlConnection(cn))
                    {
                        connection.BulkInsert(extracts);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Error(e, "Failed batch insert");
                return false;
            }
        }
    }
}