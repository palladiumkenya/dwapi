using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class DocketRepository : BaseRepository<Docket, string>, IDocketRepository
    {
        public DocketRepository(SettingsContext context) : base(context)
        {

        }
    }
}