using System;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Crs
{
    public interface IClearCrsExtracts
    {
        Task Clean(Guid extractId);
    }
}