using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Stage.Source
{
    public interface ISourceReader<T>
    {
        ReadSummary Summary { get; }
        int Find(DbProtocol protocol, DbExtract extract);
        IEnumerable<T> Read(DbProtocol protocol,DbExtract extract);
    }
}