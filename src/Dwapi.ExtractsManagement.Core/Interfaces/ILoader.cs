using System;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces
{
    public interface ILoader<in T> where T : class 
    {
        Task<int> Load(Guid extractId, int found, bool diffSupport);
    }
}
