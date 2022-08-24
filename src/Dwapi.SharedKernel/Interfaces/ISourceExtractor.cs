using System;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface ISourceExtractor<T>
    {
        Task<int> Extract(DbExtract extract, DbProtocol dbProtocol);
        Task<int> Extract(DbExtract extract, DbProtocol dbProtocol,DateTime? maxCreated, DateTime? maxModified, int siteCode);

    }
}
