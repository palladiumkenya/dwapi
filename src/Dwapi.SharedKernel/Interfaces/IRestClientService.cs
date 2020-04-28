using System.Net.Http;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface IRestClientService<T>
    {
        HttpClient Client { get; set; }
        Task<T> Read(AuthProtocol authProtocol, string url);
    }
}
