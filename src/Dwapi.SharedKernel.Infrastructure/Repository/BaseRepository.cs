using System;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Enum;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using X.PagedList;
using Z.Dapper.Plus;

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
            Dapper.SqlMapper.Settings.CommandTimeout = 0;
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
            var entities = Context.Set<T>().FromSqlRaw(sql)
                .OrderBy(x => x.Id);

            return entities.ToPagedListAsync(page ?? 1, pageSize);
        }

        public Task<IPagedList<T>> GetAll(Expression<Func<T, bool>> predicate, int? page, int pageSize)
        {
            var entities = DbSet.AsNoTracking()
                .Where(predicate)
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

        public void Create<TC>(List<TC> entity)
        {
            if (entity.Any())
            {
                Context.Database.GetDbConnection().BulkInsert(entity);
            }
        }

        public void CreateBatch(List<T> entity)
        {
            if (entity.Any())
            {
                Context.Database.GetDbConnection().BulkMerge(entity);
            }
        }

        public virtual void Update(T entity)
        {
            if (null != entity)
            {
                Context.Update(entity);
            }
        }

        public void Update<TC>(List<TC> entity)
        {
            if (entity.Any())
            {
                Context.Database.GetDbConnection().BulkUpdate(entity);
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

            if (Context.Database.IsSqlite())
            {
                using (var connection = new SqliteConnection(cn))
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

        public IDbConnection GetNewConnection()
        {
            var cn = GetConnectionString();

            if (Context.Database.IsSqlServer())
            {
                return new SqlConnection(cn);
            }

            if (Context.Database.IsMySql())
            {
                return new MySqlConnection(cn);
            }

            if (Context.Database.IsSqlite())
            {
                return new SqliteConnection(cn);
            }

            return null;
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
            return Context.Set<T>().FromSqlRaw(sql).Select(x => x.Id).CountAsync();
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

        public async Task<IEnumerable<T>> ReadAll(string exSql, int pageNumber, int pageSize)
        {
            var sql = $"{exSql} ORDER BY s.Id ";

            var sqlPaging = @"
                 OFFSET @Offset ROWS 
                 FETCH NEXT @PageSize ROWS ONLY
            ";

            if (Context.Database.IsMySql() || Context.Database.IsSqlite())
            {
                sqlPaging = @" LIMIT @PageSize OFFSET @Offset;";
            }

            sql = $"{sql}{sqlPaging}";


            using (var cn = GetNewConnection())
            {
                if(cn.State!=ConnectionState.Open)
                    cn.Open();
                var results = await cn.QueryAsync<T>(sql, new
                {
                    Offset = (pageNumber - 1) * pageSize,
                    PageSize = pageSize
                });
                return results.ToList();
            }
        }

        public string GetTableName()
        {
            // var mapping = Context.Model.FindEntityType(typeof(T)).GetTableName();
            var entityType = Context.Model.FindEntityType(typeof(T));
            var mapping = entityType?.GetSchema() + "." + entityType?.GetTableName();
            return mapping;
        }
        // public string GetTableName<T>(this DbContext dbContext) where T : class
        // {
        //     var entityType = dbContext.Model.FindEntityType(typeof(T));
        //     return entityType?.GetSchema() + "." + entityType?.GetTableName();
        // }

        public T GetSiteCode(string sql)
        {
            // return  DbSet.AsNoTracking()
            //     .FromSql(sql)
            //     .ToList().FirstOrDefault();
            return Context.Set<T>()
                .FromSqlRaw(sql)                
                .ToList()                       
                .FirstOrDefault();
            // return DbSet.AsNoTracking().Select(x => x.Id).CountAsync();
        }
    }
}
