using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Reader
{
    public interface IPsmartSourceReader : ISourceReader
    {
        IEnumerable<PsmartSource> Read(DbProtocol protocol, DbExtract extract);
    }
}