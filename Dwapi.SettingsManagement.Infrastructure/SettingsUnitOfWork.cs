using Dwapi.SettingsManagement.Core;
using Dwapi.SettingsManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.SettingsManagement.Infrastructure
{
    public class SettingsUnitOfWork : ISettingsUnitOfWork
    {
        private readonly SettingsContext _context;
        private Hashtable repositories;

        public SettingsUnitOfWork(SettingsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public DbContext Context { get { return _context; } }

        public IGenericSettingsRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericSettingsRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }

            return (IGenericSettingsRepository<T>)repositories[type];

        }

        public void Save()
            => _context.SaveChanges();

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}

