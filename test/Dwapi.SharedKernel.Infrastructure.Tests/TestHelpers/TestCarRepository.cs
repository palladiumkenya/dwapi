using System;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Tests.TestHelpers;

namespace Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers
{
    public class TestCarRepository:BaseRepository<TestCar,Guid>
    {
        public TestCarRepository(TestDbContext context) : base(context)
        {
        }
    }
}