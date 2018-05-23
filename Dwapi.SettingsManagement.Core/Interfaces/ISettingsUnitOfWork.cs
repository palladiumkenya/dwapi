using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Core.Interfaces
{
    public interface ISettingsUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
        DbContext Context { get; }
        IGenericSettingsRepository<T> Repository<T>() where T : class;
    }
}
