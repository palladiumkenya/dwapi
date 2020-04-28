using System;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs
{
    public interface IClearCbsExtracts
    {
        Task Clean(Guid extractId);
    }
}