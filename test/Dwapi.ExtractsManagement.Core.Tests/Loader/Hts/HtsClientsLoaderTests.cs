using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Hts
{
    [TestFixture]
    public class HtsClientsLoaderTests
    {
        private IHtsClientsLoader _loader;
        private IHtsClientsSourceExtractor _extractor;
        private List<Extract> _extracts;
        private DbProtocol _protocol;
        private ExtractsContext _extractsContext;
        private Extract _extract;
        private int _count;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
            _protocol = TestInitializer.Protocol;
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("HTS")).ToList();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _loader = TestInitializer.ServiceProvider.GetService<IHtsClientsLoader>();
            _extractor = TestInitializer.ServiceProvider.GetService<IHtsClientsSourceExtractor>();
            _extract = _extracts.First(x => x.Name.IsSameAs("HtsClient"));
            _count = _extractor.Extract(_extract, _protocol).Result;
        }

        [Test]
        public void should_Load()
        {
            Assert.True(_count > 0);
            Assert.False(_extractsContext.HtsClientsExtracts.Any());

            var count = _loader.Load(_extract.Id,_count).Result;

            Assert.True(count > 0);
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.AreEqual(count,_extractsContext.HtsClientsExtracts.Count());
            Log.Debug($"Temp {_count} Main {_extractsContext.HtsClientsExtracts.Count()}");
        }
    }
}
