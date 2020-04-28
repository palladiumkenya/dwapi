using System.Configuration;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests
{
    [TestFixture]
    public class SettingsContextTests
    {
        private SettingsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.SetupDb();
        }

        [SetUp]
        public void Setup()
        {
            _context = TestInitializer.ServiceProvider.GetService<SettingsContext>();
        }
        [Test]
        public void should_Seed()
        {
            _context.EnsureSeeded();
            Assert.True(_context.Dockets.Count() > 1);
            Assert.True(_context.CentralRegistries.Count() > 1);
            Assert.True(_context.EmrSystems.Count() > 1);
            Assert.True(_context.DatabaseProtocols.Count() > 1);
            Assert.True(_context.Extracts.Count() > 1);
            Assert.True(_context.RestProtocols.Count() > 1);
            Assert.True(_context.Resources.Count() > 1);
        }
    }
}
