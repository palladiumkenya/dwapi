using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Xml;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
        T Get(TId id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void CreateOrUpdate(T entity);
        
        void Delete(TId id);
        IDbConnection GetConnection();
        void SaveChanges();

    }
}
