using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Dwapi.ExtractsManagement.Core.Source.Psmart;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services.Psmart
{
    public interface IPsmartExtractService
    {

        IEnumerable<PsmartSource> Extract(DbProtocol protocol, DbExtract extract);
        void Load(IEnumerable<PsmartSource> sources,bool clearFirst=true);
        void Sync(IEnumerable<DbExtractProtocolDTO> extracts);
        string GetLoadError();
    }
}