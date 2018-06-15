using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Utilities
{
    public interface IClearDwhExtracts
    {
        Task<int> Clear(List<Guid> extractIds);
    }
}