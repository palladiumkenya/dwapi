using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using X.PagedList;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
        T Get(TId id);
        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<IPagedList<T>> GetAll(int? page, int pageSize,string sortField="",int? sortOrder=1);
        Task<IPagedList<T>> GetAll(string sql, int? page, int pageSize);
        Task<IPagedList<T>> GetAll(Expression<Func<T, bool>> predicate,int? page, int pageSize);
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
        void ExecCommand(string sql);
        TC ExecQuery<TC>(string sql);
        IEnumerable<dynamic> ExecQueryMulti<dynamic>(string sql);
       Task<int> GetCount();
       Task<int> GetCount(string sql);
       int PageCount(int batchSize, long totalRecords);
    }
}
