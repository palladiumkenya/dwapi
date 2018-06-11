using System;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Utilities
{
    public interface IClearDwhExtracts
    {
        Task<int> Clear(Guid extractId);
    }
}