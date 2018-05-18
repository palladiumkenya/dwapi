using System;
using System.Collections.Generic;
using System.Text;
using Dwapi.ExtractsManagement.Core.DTOs;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services
{
    public interface IExtractService<T>
    {
        ExtractHistory HasStarted(Guid extractId);
        ExtractHistory HasStarted(Guid extractId, ExtractStatus status, ExtractStatus otherStatus);
        void Find(DbExtractProtocolDTO extract);
        void Sync(DbExtractProtocolDTO extract);
        void Find(IEnumerable<DbExtractProtocolDTO> extracts);
        void Sync(IEnumerable<DbExtractProtocolDTO> extracts);
        ExtractEventDTO GetStatus(Guid extractId);
        void Complete(Guid extractId);
        void Clear();
        string GetLoadError();
        IEnumerable<T> Extract(DbProtocol protocol, DbExtract extract);
       
    }
}
