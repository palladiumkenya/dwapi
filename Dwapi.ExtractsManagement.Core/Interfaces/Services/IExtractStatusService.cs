using Dwapi.ExtractsManagement.Core.DTOs;
using System;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services
{
    public interface IExtractStatusService
    {
        ExtractEventDTO GetStatus(Guid extractId);

    }
}