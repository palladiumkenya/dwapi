using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Validators.Cbs
{
    public interface IMasterPatientIndexValidator
    {

        Task<bool> Validate(string sourceTable);
    }
}