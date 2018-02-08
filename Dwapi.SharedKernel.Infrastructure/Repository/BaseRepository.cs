using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SharedKernel.Infrastructure.Repository
{
    public abstract class BaseRepository<T, TId> : IRepository<T, TId> where T : Entity<TId>
    {
        protected internal DbContext Context;
        protected internal DbSet<T> DbSet;

        protected BaseRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

//        public virtual T Get(TId id, ReadState readState = ReadState.NonVoided)
//        {
//            return GetAll(x => x.Id.Equals(id), readState)
//               .FirstOrDefault();
//        }

//        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, ReadState readState = ReadState.NonVoided)
//        {
//            var result= DbSet.AsNoTracking().Where(predicate);
//           
//            if (readState == ReadState.NonVoided)
//            {
//                return result.Where(x => null == x.Voided || null != x.Voided && x.Voided == false);
//            }
//            if (readState == ReadState.Voided)
//            {
//                return result.Where(x => null != x.Voided && x.Voided == true);
//            }
//            return result;
//        }
//
        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }
//
//        public int GetCount()
//        {
//            return DbSet.AsNoTracking().Select(x => x.Id).Count();
//        }
//
//        public virtual void Create(T entity, string createdBy = "sys")
//        {
//            if (null != entity)
//            {
//                entity.CreatedDate = DateTime.Now;
//                entity.CreatedBy = createdBy;
//                Context.Add(entity);
//            }
//        }
//
//        public virtual void Create(IEnumerable<T> entities, string createdBy = "sys")
//        {
//            if (null != entities)
//            {
//                var entitiesToCreate = entities.ToList();
//                foreach (var e in entitiesToCreate)
//                {
//                    e.CreatedDate = DateTime.Now;
//                    e.CreatedBy = createdBy;
//                }
//                Context.AddRange(entitiesToCreate);
//            }          
//        }
//
//        public virtual void Update(T entity, string modifiedBy = "sys")
//        {
//            if (null != entity)
//            {
//                entity.ModifiedDate = DateTime.Now;
//                entity.ModifiedBy = modifiedBy;
//                Context.Update(entity);
//            }
//        }
//
//        public virtual void Update(IEnumerable<T> entities, string modifiedBy = "sys")
//        {
//            if (null != entities)
//            {
//                var entitiesToUpdate = entities.ToList();
//                foreach (var e in entitiesToUpdate)
//                {
//                    e.ModifiedDate = DateTime.Now;
//                    e.ModifiedBy = modifiedBy;
//                }
//                Context.UpdateRange(entitiesToUpdate);
//            }
//        }
//
//        public virtual void Delete(TId id)
//        {
//            var entity = DbSet.Find(id);
//            Delete(entity);
//        }
//
//        public virtual void Delete(IEnumerable<TId> entities)
//        {
//            var entitiesToDelete =new List<T>();
//            foreach (var id in entities)
//            {
//                var entity = DbSet.Find(id); 
//                if (null != entity)
//                    entitiesToDelete.Add(entity);
//            }
//
//            Delete(entitiesToDelete);
//        }
//
//        public virtual void Delete(T entity)
//        {
//            if (null != entity)
//                DbSet.Remove(entity);
//        }
//
//        public virtual void Delete(IEnumerable<T> entities)
//        {
//            if (null != entities)
//            {
//                var entitiesToDelete = entities.ToList();
//
//                if (entitiesToDelete.Count > 0)
//                    DbSet.RemoveRange(entitiesToDelete);
//            }
//        }
//
//        public virtual void Void(TId id, string voidedBy = "sys")
//        {
//            var entity = DbSet.Find(id);
//            Void(entity, voidedBy);
//        }
//
//        public virtual void Void(IEnumerable<TId> entities, string voidedBy = "sys")
//        {
//            var entitiesToVoid = new List<T>();
//            foreach (var id in entities)
//            {
//                var entity = DbSet.Find(id);
//                if (null != entity)
//                    entitiesToVoid.Add(entity);
//            }
//            Void(entitiesToVoid, voidedBy);
//        }
//
//        public virtual void Void(T entity, string voidedBy = "sys")
//        {
//            if (null != entity)
//            {
//                entity.Voided = true;
//                entity.VoidedDate=DateTime.Now;
//                entity.VoidedBy = voidedBy;
//                Update(entity, voidedBy);
//            }
//        }
//
//        public virtual void Void(IEnumerable<T> entities, string voidedBy = "sys")
//        {
//            if (null != entities)
//            {
//                var entitiesToVoid = entities.ToList();
//
//                foreach (var e in entitiesToVoid)
//                {
//                    e.Voided = true;
//                    e.VoidedDate = DateTime.Now;
//                    e.VoidedBy = voidedBy;
//                }
//
//                if (entitiesToVoid.Count > 0)
//                    Update(entitiesToVoid, voidedBy);
//            }
//        }
//
//        public bool CriteriaExisits(T entity, Expression<Func<T, bool>> predicate, bool excluedSelf = false)
//        {
//            if (null == entity)
//                return false;
//
//            var nameExisits = GetAll(predicate);
//
//            if (excluedSelf)
//                nameExisits = nameExisits.Where(x => !x.Id.Equals(entity.Id));
//
//            return nameExisits.ToList().Count > 0;
//        }
//
//        public virtual void Save()
//        {
//            Context.SaveChanges();
//        }
    }
}