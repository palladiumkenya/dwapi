using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Source.Psmart;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services.Psmart
{
    public interface IPsmartExtractService
    {
        ExtractHistory HasStarted(Guid extractId);
        void Find(IEnumerable<DbExtractProtocolDTO> extracts);
        void Sync(IEnumerable<DbExtractProtocolDTO> extracts);

        IEnumerable<PsmartSource> Extract(DbProtocol protocol, DbExtract extract);
        
        int Load(IEnumerable<PsmartSource> sources,bool clearFirst=true);
    
        string GetLoadError();
    }
}