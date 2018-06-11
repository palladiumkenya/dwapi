using System;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Validators
{
    public interface IExtractValidator
    {
        Task<bool> Validate(Guid extractId, int extracted, string extractName, string sourceTable);
    }
}