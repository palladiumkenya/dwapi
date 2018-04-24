using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Model
{
    public abstract class Entity<TId>
    {
        [Key, Column(Order = 0, TypeName = "char(38)")]
        public virtual TId Id { get; set; }

        protected Entity()
        {
           Type idType = typeof(TId);
            if (idType ==typeof(Guid))
            {
                Id = (TId)(object)LiveGuid.NewGuid();
            }
        }
        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<TId>;
            if (entity != null)
            {
                return this.Equals(entity);
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #region IEquatable<Entity> Members
        public bool Equals(Entity<TId> other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }
        #endregion
    }
}