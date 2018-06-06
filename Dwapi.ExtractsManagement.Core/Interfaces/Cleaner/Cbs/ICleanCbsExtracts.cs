using System;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs
{
    public interface ICleanCbsExtracts
    {
        Task Clean(Guid extractId);
    }
}