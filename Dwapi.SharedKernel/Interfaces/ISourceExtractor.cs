using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface ISourceExtractor<T>
    {
        Task<int> Extract(DbExtract extract, DbProtocol dbProtocol);
        Task<int> ReadExtract(DbExtract extract, DbProtocol dbProtocol);

    }
}
