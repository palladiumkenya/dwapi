using Microsoft.EntityFrameworkCore;

namespace Dwapi.SharedKernel.Infrastructure
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {
        }

        public virtual void EnsureSeeded()
        {
            
        }
    }
}
