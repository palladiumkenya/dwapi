using System;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Enum;
using MySql.Data.MySqlClient;
using X.PagedList;

namespace Dwapi.SharedKernel.Infrastructure.Repository
{
    public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : Entity<TId>
    {
        protected internal DbContext Context;
        protected internal DbSet<T> DbSet;
        private IDbConnection _connection;

        protected BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual T Get(TId id)
        {
            return DbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsNoTracking().FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsNoTracking();
        }

        public Task<IPagedList<T>> GetAll(int? page, int pageSize, string sortField="", int? sortOrder=1)
        {
            var entities = DbSet.AsNoTracking()
                .OrderBy(x => x.Id);

            return entities.ToPagedListAsync(page ?? 1, pageSize);
        }

        public Task<IPagedList<T>> GetAll(string sql, int? page, int pageSize)
        {
            var entities = DbSet.AsNoTracking().FromSql(sql)
                .OrderBy(x => x.Id);

            return entities.ToPagedListAsync(page ?? 1, pageSize);
        }

        public virtual void Create(T entity)
        {
            if (null != entity)
            {
                Context.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            if (null != entity)
            {
                Context.Update(entity);
            }
        }

        public virtual void CreateOrUpdate(T entity)
        {
            if (null == entity)
                return;

            var exists = DbSet.AsNoTracking().FirstOrDefault(x => Equals(x.Id, entity.Id));
            if (null != exists)
            {
                Update(entity);
                return;
            }

            Create(entity);
        }

        public IEnumerable<T> GetFromSql(string query)
        {
            IEnumerable<T> results = Enumerable.Empty<T>();

            var cn = GetConnectionString();

            if (Context.Database.ProviderName.ToLower().Contains("SqlServer".ToLower()))
            {
                using (var connection = new SqlConnection(cn))
                {
                    results = connection.Query<T>(query);

                }
            }

            if (Context.Database.ProviderName.ToLower().Contains("MySql".ToLower()))
            {
                using (var connection = new MySqlConnection(cn))
                {
                    results = connection.Query<T>(query);

                }
            }

            return results;
        }

        public DatabaseProvider GetConnectionProvider()
        {
            DatabaseProvider provider = DatabaseProvider.Other;
            if (Context.Database.ProviderName.ToLower().Contains("SqlServer".ToLower()))
            {
                provider = DatabaseProvider.MsSql;
            }

            if (Context.Database.ProviderName.ToLower().Contains("MySql".ToLower()))
            {
                provider = DatabaseProvider.MySql;
            }
            return provider;
        }

        public virtual void Delete(TId id)
        {
            var entity = DbSet.Find(id);
            Delete(entity);
        }

        public IDbConnection GetConnection(bool opened = true)
        {
            if (null == _connection)
            {
                _connection = Context.Database.GetDbConnection();
            }

            if (opened)
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
            }
            return _connection;
        }

        public string GetConnectionString()
        {
            return Context.Database.GetDbConnection().ConnectionString;
        }

        public void CloseConnection()
        {
            CloseConnection(_connection);
        }

        public void CloseConnection(IDbConnection connection)
        {
            if (null != connection)
            {
                try
                {
                    connection.Close();
                }
                catch { }
            }
        }

        public virtual void Delete(T entity)
        {
            if (null != entity)
                DbSet.Remove(entity);
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public void ExecCommand(string sql)
        {
            GetConnection().Execute(sql);
        }

        public TC ExecQuery<TC>(string sql)
        {
           return GetConnection().Query<TC>(sql).FirstOrDefault();
        }

        public IEnumerable<dynamic> ExecQueryMulti<dynamic>(string sql)
        {
            return GetConnection().Query<dynamic>(sql);

        }
        public Task<int> GetCount()
        {
            return DbSet.AsNoTracking().Select(x => x.Id).CountAsync();
        }

        public Task<int> GetCount(string sql)
        {
            return DbSet.AsNoTracking().FromSql(sql).Select(x => x.Id).CountAsync();
        }

        public int PageCount(int batchSize, long totalRecords)
        {
            if (totalRecords > 0) {
                if (totalRecords < batchSize) {
                    return 1;
                }
                return (int)Math.Ceiling(totalRecords / (double)batchSize);
            }
            return 0;
        }
    }
}
