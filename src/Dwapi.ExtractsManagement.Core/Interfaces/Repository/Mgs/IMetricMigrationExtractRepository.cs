using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs
{
    public interface IMetricMigrationExtractRepository : IRepository<MetricMigrationExtract, Guid>
    {
        bool BatchInsert(IEnumerable<MetricMigrationExtract> extracts);
        IEnumerable<MetricMigrationExtract> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}
