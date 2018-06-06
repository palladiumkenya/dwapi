using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository
{
    public interface IPsmartStageRepository:IStageRepository<PsmartStage>
    {
        void UpdateStatus(IEnumerable<Guid> eids,bool sent, string requestId);
    }
}