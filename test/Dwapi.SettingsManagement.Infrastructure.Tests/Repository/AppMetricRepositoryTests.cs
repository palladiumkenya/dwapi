using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Infrastructure.Tests.TestArtifacts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class AppMetricRepositoryTests
    {
        private IAppMetricRepository _appMetricRepository;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateAppMetrics());
        }

        [SetUp]
        public void SetUp()
        {
            _appMetricRepository = TestInitializer.ServiceProvider.GetService<IAppMetricRepository>();
        }

        [Test]
        public void should_Load_current()
        {
            var mets = _appMetricRepository.GetAll().ToList();
            Assert.True(mets.Any());
        }
    }
}
