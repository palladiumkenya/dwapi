using System;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Indicator;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Indicator
{
    public class IndicatorsRepository : BaseRepository<IndicatorExtract, Guid>, IIndicatorsRepository
    {
        public IndicatorsRepository(ExtractsContext context) : base(context)
        {
        }

        public IndicatorExtract GetIndicatorValue(string name)
        {
            return Get(x => x.Indicator.ToLower() == name.ToLower());
        }
    }
}
