using Dwapi.ExtractsManagement.Core.DTOs;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using System;
using Dwapi.ExtractsManagement.Core.Commands;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services
{
    public interface IExtractStatusService
    {
        ExtractEventDTO GetStatus(Guid extractId);

    }
}