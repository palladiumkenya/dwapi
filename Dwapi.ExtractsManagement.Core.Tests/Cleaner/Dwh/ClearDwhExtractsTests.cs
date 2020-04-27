using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Cleaner.Mgs
{
    [TestFixture]
    public class ClearMgsExtractsTests
    {
        private IClearMgsExtracts _clearMgs;
        private List<Extract> _extracts;
        private ExtractsContext _extractsContext;
       

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("MGS")).ToList();
            TestInitializer.LoadMgs();
        }

        [SetUp]
        public void SetUp()
        {
            _clearMgs = TestInitializer.ServiceProvider.GetService<IClearMgsExtracts>();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [Test]
        public void should_clean()
        {
            var extractIds = _extracts.Select(x => x.Id).ToList();
            Assert.True(_extractsContext.TempMetricMigrationExtracts.Any());
            Assert.True(_extractsContext.MetricMigrationExtracts.Any());

            _clearMgs.Clear(extractIds).Wait();
            
            Assert.False(_extractsContext.TempMetricMigrationExtracts.Any());
            Assert.False(_extractsContext.MetricMigrationExtracts.Any());
            Assert.False(_extractsContext.ExtractHistory.Any(x => extractIds.Contains(x.ExtractId)));
        }

    }
}