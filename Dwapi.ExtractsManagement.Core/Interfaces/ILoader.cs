using System.Threading.Tasks;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface ILoader<in T> where T : class 
    {
        Task<int> Load(int found);
    }
}