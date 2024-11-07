using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.SharedKernel.Infrastructure.Repository;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts
{
    [Obsolete]
    public class TempHTSClientLinkageExtractRepository: BaseRepository<TempHTSClientLinkageExtract, Guid>, ITempHTSClientLinkageExtractRepository
    {
        public TempHTSClientLinkageExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public bool BatchInsert(IEnumerable<TempHTSClientLinkageExtract> extracts)
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
