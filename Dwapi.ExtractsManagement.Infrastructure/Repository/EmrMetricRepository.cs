using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model.Destination;
using Dwapi.SharedKernel.Infrastructure.Repository;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository
{
    public class EmrMetricRepository:BaseRepository<EmrMetric,Guid>, IEmrMetricRepository
    {
        public EmrMetricRepository(ExtractsContext context) : base(context)
        {
        }

        public override void CreateOrUpdate(EmrMetric entity)
        {
            var ctx = Context as ExtractsContext;
            var allmetrics = ctx.EmrMetrics;

            if (allmetrics.Any())
            {
                ctx.RemoveRange(allmetrics);
                ctx.SaveChanges();
            }

            ctx.EmrMetrics.Add(entity);
            SaveChanges();
        }
    }
}
