using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs
{
    public interface IMasterPatientIndexValidator
    {

        Task<bool> Validate();
        Task<int> GetRejectedCount();
    }
}