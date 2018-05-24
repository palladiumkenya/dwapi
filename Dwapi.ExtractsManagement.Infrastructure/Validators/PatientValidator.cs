using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;

namespace Dwapi.ExtractsManagement.Infrastructure.Validators
{
    public class PatientValidator: IPatientValidator
    {
        public Task<int> Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}