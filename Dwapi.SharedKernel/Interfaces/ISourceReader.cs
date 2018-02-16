using System.Collections.Generic;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface ISourceReader<T>
    {
        int Find(DbProtocol protocol, DbExtract extract);
        IEnumerable<T> Read(DbProtocol protocol,DbExtract extract);
    }
}