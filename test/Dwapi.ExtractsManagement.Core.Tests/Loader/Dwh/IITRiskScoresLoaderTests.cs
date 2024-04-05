using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Dwh
{
    [TestFixture]
    public class IITRiskScoressLoaderTests
    {
      private IIITRiskScoresLoader _loader;
        private IIITRiskScoresSourceExtractor _extractor;
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
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("NDWH")).ToList();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();

            var patientExtract = _extracts.First(x => x.Name.IsSameAs(nameof(PatientExtract)));
            var patientLoader = TestInitializer.ServiceProvider.GetService<IPatientLoader>();
            var patientSourceExtractor = TestInitializer.ServiceProvider.GetService<IPatientSourceExtractor>();
            var tempCount = patientSourceExtractor.Extract(patientExtract, _protocol).Result;
            var patientCount = patientLoader.Load(patientExtract.Id, tempCount, false).Result;
        }

        [SetUp]
        public void SetUp()
        {
            _loader = TestInitializer.ServiceProvider.GetService<IIITRiskScoresLoader>();
            _extractor = TestInitializer.ServiceProvider.GetService<IIITRiskScoresSourceExtractor>();
            _extract = _extracts.First(x => x.Name.IsSameAs(nameof(IITRiskScoresExtract)));
            _count = _extractor.Extract(_extract, _protocol).Result;

        }

        [Test]
        public void should_Load()
        {
            Assert.True(_count > 0);
            Assert.False(_extractsContext.IITRiskScoresExtracts.Any());

            var count = _loader.Load(_extract.Id,_count, false).Result;

            Assert.True(count >0);
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.AreEqual(count,_extractsContext.IITRiskScoresExtracts.Count());
            Log.Debug($"Temp {_count} Main {_extractsContext.IITRiskScoresExtracts.Count()}");
        }
    }
}
