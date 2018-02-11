using System.Collections.Generic;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Stage.Repository
{
    public interface IStageRepository<T, in TId> where T : Entity<TId>
    {
        void Clear(string emr);
        void Load(T entity);
        void Load(IEnumerable<T> entities);
        void SaveChanges();
    }
}