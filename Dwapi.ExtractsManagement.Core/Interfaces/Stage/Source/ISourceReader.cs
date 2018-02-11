using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Source;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Source
{
    public interface ISourceReader<T>
    {
        ReadSummary Summary { get; }
        IEnumerable<T> Read(DbProtocol protocol,DbExtract extract);
    }
}