using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Tests.TestHelpers;

namespace Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers
{
    public class TestCountyRepository:BaseRepository<TestCounty, int>
    {
        public TestCountyRepository(TestDbContext context) : base(context)
        {
        }
    }
}