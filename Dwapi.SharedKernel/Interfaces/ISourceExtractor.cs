using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface ISourceExtractor<T>
    {
        Task<bool> Extract(DbExtract extract, DbProtocol dbProtocol);
    }
}