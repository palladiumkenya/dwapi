using Dwapi.ExtractsManagement.Core;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Infrastructure
{
    public class ExtractUnitOfWork : IExtractUnitOfWork
    {
        private readonly ExtractsContext _context;
        private Hashtable repositories;

        public ExtractUnitOfWork(ExtractsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public DbContext Context { get { return _context; } }

        public IGenericExtractRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericExtractRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }

            return (IGenericExtractRepository<T>)repositories[type];

        }

        public void Save()
            => _context.SaveChanges();

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
