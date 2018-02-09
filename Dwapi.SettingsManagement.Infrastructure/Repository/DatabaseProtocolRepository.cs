using System;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class DatabaseProtocolRepository : BaseRepository<DatabaseProtocol, Guid>, IDatabaseProtocolRepository
    {
        public DatabaseProtocolRepository(DbContext context) : base(context)
        {
        }
    }
}