using System.Collections.Generic;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface IStageRepository<T>
    {
        void Clear(string emr="");
        void Load(T entity);
        void Load(IEnumerable<T> entities);
        void SaveChanges();
        IEnumerable<T> GetAll();
        int Count(string emr);
    }
}