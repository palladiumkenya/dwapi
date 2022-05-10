using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Crs
{
    public class ClientRegistryExtractRepository : BaseRepository<ClientRegistryExtract, Guid>, IClientRegistryExtractRepository
    {
        public ClientRegistryExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<ClientRegistryExtract> extracts)
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

        public IEnumerable<ClientRegistryExtract> GetView()
        {
            var ctx = Context as ExtractsContext;
            return ctx.ClientRegistryExtracts.FromSql("select * from ClientRegistryExtracts");
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