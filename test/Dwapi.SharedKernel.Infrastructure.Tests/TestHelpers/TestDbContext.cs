using Dwapi.SharedKernel.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers
{
    public class TestDbContext:DbContext
    {
        public DbSet<TestCar> TestCars { get; set; }
        public DbSet<TestCounty> TestCounties { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
    }
}