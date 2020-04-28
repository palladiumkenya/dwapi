using System.Linq;
using Dwapi.ExtractsManagement.Infrastructure.Tests.TestArtifacts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests
{
    [TestFixture]
    public class ExtractsContextTests
    {
        private ExtractsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            //TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
        }

        [SetUp]
        public void Setup()
        {
            _context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }
        [Test]
        public void should_Seed()
        {
            _context.EnsureSeeded();
            Assert.True(_context.Validator.Count() > 1);
        }
    }
}
