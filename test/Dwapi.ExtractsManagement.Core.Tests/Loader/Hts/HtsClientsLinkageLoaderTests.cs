using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
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
    public class HtsClientsLinkageLoaderTests
    {
        private IHtsClientsLinkageLoader _loader;
        private IHtsClientsLinkageSourceExtractor _extractor;
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

            var clientExtract = _extracts.First(x => x.Name.IsSameAs("HtsClient"));
            var clientLoader = TestInitializer.ServiceProvider.GetService<IHtsClientsLoader>();
            var clientsSourceExtractor = TestInitializer.ServiceProvider.GetService<IHtsClientsSourceExtractor>();
            var tempCount = clientsSourceExtractor.Extract(clientExtract, _protocol).Result;
            var patientCount = clientLoader.Load(clientExtract.Id, tempCount, false).Result;
        }

        [SetUp]
        public void SetUp()
        {
            _loader = TestInitializer.ServiceProvider.GetService<IHtsClientsLinkageLoader>();
            _extractor = TestInitializer.ServiceProvider.GetService<IHtsClientsLinkageSourceExtractor>();
            _extract = _extracts.First(x => x.Name.IsSameAs(nameof(HtsClientLinkage)));
            _count = _extractor.Extract(_extract, _protocol).Result;
        }

        [Test]
        public void should_Load()
        {
            Assert.True(_count > 0);
            Assert.False(_extractsContext.HtsClientsLinkageExtracts.Any());

            var count = _loader.Load(_extract.Id,_count, false).Result;

            Assert.True(count > 0);
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.AreEqual(count,_extractsContext.HtsClientsLinkageExtracts.Count());
            Log.Debug($"Temp {_count} Main {_extractsContext.HtsClientsLinkageExtracts.Count()}");
        }
    }
}
