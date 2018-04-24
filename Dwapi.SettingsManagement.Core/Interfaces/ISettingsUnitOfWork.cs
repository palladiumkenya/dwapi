using Dwapi.SettingsManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.SettingsManagement.Core
{
    public interface ISettingsUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
        DbContext Context { get; }
        IGenericSettingsRepository<T> Repository<T>() where T : class;
    }
}
