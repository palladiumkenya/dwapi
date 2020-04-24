using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.UploadManagement.Core.Exchange.Mgs
{
    public class MgsMessage
    {
        public List<MetricMigrationExtract> Migrations { get; set; } = new List<MetricMigrationExtract>();

        public MgsMessage()
        {
        }

        public MgsMessage(List<MetricMigrationExtract> migrations)
        {
            Migrations = migrations;
        }

        public static List<MgsMessage> Create(List<MetricMigrationExtract> clients)
        {
            var list = new List<MgsMessage>();
            var chunks = clients.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MgsMessage(chunk));
            }

            return list;
        }
    }
}
