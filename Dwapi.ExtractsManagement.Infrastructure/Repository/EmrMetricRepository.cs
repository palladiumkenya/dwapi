using System;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository
{
    public class EmrMetricRepository:BaseRepository<EmrMetric,Guid>, IEmrMetricRepository
    {
        public EmrMetricRepository(ExtractsContext context) : base(context)
        {
        }

        public override void CreateOrUpdate(EmrMetric entity)
        {
            var metrics = GetAll();
            foreach (var emrMetric in metrics)
            {
                Delete(emrMetric);
                SaveChanges();
            }

            base.CreateOrUpdate(entity);
            SaveChanges();
        }
    }
}