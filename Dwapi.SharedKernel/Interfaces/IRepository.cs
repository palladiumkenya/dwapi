using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Interfaces
{
    public interface IRepository<T, in TId> where T : Entity<TId>
    {
//        T Get(TId id);

        IEnumerable<T> GetAll();

//        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
//       
//        int GetCount();
//        
        void Create(T entity);

//        void Create(IEnumerable<T> entities);
//
//        void Update(T entity);
//        void Update(IEnumerable<T> entities);
//
//        void Delete(TId id);
//        void Delete(IEnumerable<TId> entities);
//        void Delete(T entity);
//        void Delete(IEnumerable<T> entities);
//
//
        void SaveChanges();
    }
}