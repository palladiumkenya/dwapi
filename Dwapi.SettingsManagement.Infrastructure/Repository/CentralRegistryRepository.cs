using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class CentralRegistryRepository : BaseRepository<CentralRegistry, Guid>, ICentralRegistryRepository
    {
        public CentralRegistryRepository(SettingsContext context) : base(context)
        {
        }

        public CentralRegistry GetDefault()
        {
           return GetAll().FirstOrDefault();
        }

        public void SaveDefault(CentralRegistry centralRegistry)
        {
            if (GetAll().Any())
            {
                Context.RemoveRange(GetAll());
                Context.SaveChanges();
            }
            Create(centralRegistry);
        }
    }
}