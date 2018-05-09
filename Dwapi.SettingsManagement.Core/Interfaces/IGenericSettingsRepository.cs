using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.SettingsManagement.Core.Interfaces
{
    public interface IGenericSettingsRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
        Task<TEntity> FindByIdAsync(object id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includeProperties = null,
           int? page = null,
           int? pageSize = null);
    }
}
