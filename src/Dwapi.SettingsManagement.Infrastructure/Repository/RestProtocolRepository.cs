using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class RestProtocolRepository : BaseRepository<RestProtocol, Guid>, IRestProtocolRepository
    {
        public RestProtocolRepository(SettingsContext context) : base(context)
        {
        }
        
        public void UpdateResource(Resource resource)
        {
            var ctx = Context as SettingsContext;
            var exists = ctx.Resources.AsNoTracking().FirstOrDefault(x => Equals(x.Id, resource.Id));
            if (null != exists)
            {
                ctx.Resources.Update(resource);
                return;
            }
            ctx.Resources.Add(resource);
        }
    }
}
