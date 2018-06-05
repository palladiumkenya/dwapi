using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Utilities.Cbs
{
    public interface IClearCbsExtracts
    {
        Task<int> Clear();
    }
}