using System;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class RestProtocolRepository : BaseRepository<RestProtocol, Guid>, IRestProtocolRepository
    {
        public RestProtocolRepository(SettingsContext context) : base(context)
        {
        }
    }
}