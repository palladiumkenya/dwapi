using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core
{
    public interface IExtractUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
        DbContext Context { get; }
        IGenericExtractRepository<T> Repository<T>() where T : class;
    }
}
