using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Validators
{
    public interface IPatientValidator
    {
        Task<bool> Validate(int extracted);
        Task<int> GetRejectedCount();
    }
}