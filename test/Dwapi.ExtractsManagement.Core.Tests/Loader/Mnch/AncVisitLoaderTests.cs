using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Tests.Loader.Mnch
{
    [TestFixture]
    public class AncVisitLoaderTests
    {
      private IAncVisitLoader _loader;
        private IAncVisitSourceExtractor _extractor;
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
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("MNCH")).ToList();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();

            var patientExtract = _extracts.First(x => x.Name.IsSameAs(nameof(PatientMnchExtract)));
            var patientLoader = TestInitializer.ServiceProvider.GetService<IPatientMnchLoader>();
            var patientSourceExtractor = TestInitializer.ServiceProvider.GetService<IPatientMnchSourceExtractor>();
            var tempCount = patientSourceExtractor.Extract(patientExtract, _protocol).Result;
            var patientCount = patientLoader.Load(patientExtract.Id, tempCount, false).Result;
        }

        [SetUp]
        public void SetUp()
        {
            _loader = TestInitializer.ServiceProvider.GetService<IAncVisitLoader>();
            _extractor = TestInitializer.ServiceProvider.GetService<IAncVisitSourceExtractor>();
            _extract = _extracts.First(x => x.Name.IsSameAs(nameof(AncVisitExtract)));
            _count = _extractor.Extract(_extract, _protocol).Result;

        }

        [Test]
        public void should_Load()
        {
            Assert.True(_count > 0);
            Assert.False(_extractsContext.AncVisitExtracts.Any());

            var count = _loader.Load(_extract.Id,_count, false).Result;

            Assert.True(count >0);
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.AreEqual(count,_extractsContext.AncVisitExtracts.Count());
            Log.Debug($"Temp {_count} Main {_extractsContext.AncVisitExtracts.Count()}");
        }
    }
}
