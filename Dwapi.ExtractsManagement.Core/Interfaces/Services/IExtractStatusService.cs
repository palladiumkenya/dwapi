using Dwapi.ExtractsManagement.Core.DTOs;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using System;
using Dwapi.ExtractsManagement.Core.Commands;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services
{
    public interface IExtractStatusService
    {
        ExtractHistory HasStarted(Guid extractId);

        ExtractHistory HasStarted(Guid extractId, ExtractStatus status, ExtractStatus otherStatus);

        ExtractEventDTO GetStatus(Guid extractId);

        void Found(DwhExtract extract, int found);

        void Complete(Guid extractId);
    }
}