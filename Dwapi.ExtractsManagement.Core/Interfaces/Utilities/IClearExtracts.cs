using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Utilities
{
    public interface IClearExtracts
    {
        Task<int> Clear();
    }
}