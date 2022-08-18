using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Validators.Crs
{
    public interface IClientRegistryExtractValidator
    {

        Task<bool> Validate(string sourceTable);
    }
}