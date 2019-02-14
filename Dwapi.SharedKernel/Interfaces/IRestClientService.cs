using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface IRestClientService<T>
    {
        Task<T> Read(AuthProtocol authProtocol, string url);
    }
}
