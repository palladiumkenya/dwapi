using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
        T Get(TId id);
        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Update(T entity);
        void CreateOrUpdate(T entity);
        IEnumerable<T> GetFromSql(string query);
        DatabaseProvider GetConnectionProvider();
        void Delete(TId id);
        IDbConnection GetConnection(bool opened=true);
        string GetConnectionString();
        void CloseConnection();
        void CloseConnection(IDbConnection connection);
        void SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
