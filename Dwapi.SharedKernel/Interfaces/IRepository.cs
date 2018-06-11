using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
        T Get(TId id);
        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void CreateOrUpdate(T entity);
        List<T> GetFromSql(string query);
        void Delete(TId id);
        IDbConnection GetConnection();
        string GetConnectionString();
        void CloseConnection();
        void SaveChanges();

    }
}
