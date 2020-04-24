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
    public class PatientLoaderTests
    {
        private IPatientLoader _loader;
        private IPatientSourceExtractor _extractor;
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
        }

        [SetUp]
        public void SetUp()
        {
            _loader = TestInitializer.ServiceProvider.GetService<IPatientLoader>();
            _extractor = TestInitializer.ServiceProvider.GetService<IPatientSourceExtractor>();
            _extract = _extracts.First(x => x.Name.IsSameAs(nameof(PatientExtract)));
            _count = _extractor.Extract(_extract, _protocol).Result;
        }

        [Test]
        public void should_Load()
        {
            Assert.True(_count > 0);
            Assert.False(_extractsContext.PatientExtracts.Any());

            var count = _loader.Load(_extract.Id,_count).Result;

            Assert.True(count >= _count);
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.AreEqual(count,_extractsContext.PatientExtracts.Count());
            Log.Debug($"Temp {_count} Main {_extractsContext.PatientExtracts.Count()}");
        }
    }
}
