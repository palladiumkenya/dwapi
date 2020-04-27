using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Cleaner.Dwh
{
    [TestFixture]
    public class ClearDwhExtractsTests
    {
        private IClearDwhExtracts _clearDwh;
        private List<Extract> _extracts;
        private ExtractsContext _extractsContext;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("NDWH")).ToList();
            TestInitializer.LoadCt();
        }

        [SetUp]
        public void SetUp()
        {
            _clearDwh = TestInitializer.ServiceProvider.GetService<IClearDwhExtracts>();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [Test]
        public void should_clean()
        {
            var extractIds = _extracts.Select(x => x.Id).ToList();

            Assert.True(_extractsContext.TempPatientExtracts.Any());
            Assert.True(_extractsContext.TempPatientArtExtracts.Any());
            Assert.True(_extractsContext.PatientArtExtracts.Any());
            Assert.True(_extractsContext.TempPatientBaselinesExtracts.Any());
            Assert.True(_extractsContext.PatientBaselinesExtracts.Any());
            Assert.True(_extractsContext.TempPatientStatusExtracts.Any());
            Assert.True(_extractsContext.PatientStatusExtracts.Any());
            Assert.True(_extractsContext.TempPatientLaboratoryExtracts.Any());
            Assert.True(_extractsContext.PatientLaboratoryExtracts.Any());
            Assert.True(_extractsContext.TempPatientPharmacyExtracts.Any());
            Assert.True(_extractsContext.PatientPharmacyExtracts.Any());
            Assert.True(_extractsContext.TempPatientVisitExtracts.Any());
            Assert.True(_extractsContext.PatientVisitExtracts.Any());
            Assert.True(_extractsContext.TempPatientAdverseEventExtracts.Any());
            Assert.True(_extractsContext.PatientAdverseEventExtracts.Any());
            Assert.True(_extractsContext.PatientExtracts.Any());

            _clearDwh.Clear(extractIds).Wait();

            Assert.False(_extractsContext.TempPatientExtracts.Any());
            Assert.False(_extractsContext.TempPatientArtExtracts.Any());
            Assert.False(_extractsContext.PatientArtExtracts.Any());
            Assert.False(_extractsContext.TempPatientBaselinesExtracts.Any());
            Assert.False(_extractsContext.PatientBaselinesExtracts.Any());
            Assert.False(_extractsContext.TempPatientStatusExtracts.Any());
            Assert.False(_extractsContext.PatientStatusExtracts.Any());
            Assert.False(_extractsContext.TempPatientLaboratoryExtracts.Any());
            Assert.False(_extractsContext.PatientLaboratoryExtracts.Any());
            Assert.False(_extractsContext.TempPatientPharmacyExtracts.Any());
            Assert.False(_extractsContext.PatientPharmacyExtracts.Any());
            Assert.False(_extractsContext.TempPatientVisitExtracts.Any());
            Assert.False(_extractsContext.PatientVisitExtracts.Any());
            Assert.False(_extractsContext.TempPatientAdverseEventExtracts.Any());
            Assert.False(_extractsContext.PatientAdverseEventExtracts.Any());
            Assert.False(_extractsContext.PatientExtracts.Any());
            Assert.False(_extractsContext.ExtractHistory.Any(x => extractIds.Contains(x.ExtractId)));
        }

    }
}
