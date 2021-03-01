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

            Assert.True(_extractsContext.TempAllergiesChronicIllnessExtracts.Any());
            Assert.True(_extractsContext.AllergiesChronicIllnessExtracts.Any());
            Assert.True(_extractsContext.TempContactListingExtracts.Any());
            Assert.True(_extractsContext.ContactListingExtracts.Any());
            Assert.True(_extractsContext.DepressionScreeningExtracts.Any());
            Assert.True(_extractsContext.TempDepressionScreeningExtracts.Any());
            Assert.True(_extractsContext.TempDrugAlcoholScreeningExtracts.Any());
            Assert.True(_extractsContext.DrugAlcoholScreeningExtracts.Any());
            Assert.True(_extractsContext.TempEnhancedAdherenceCounsellingExtracts.Any());
            Assert.True(_extractsContext.EnhancedAdherenceCounsellingExtracts.Any());
            Assert.True(_extractsContext.TempGbvScreeningExtracts.Any());
            Assert.True(_extractsContext.GbvScreeningExtracts.Any());
            Assert.True(_extractsContext.TempIptExtracts.Any());
            Assert.True(_extractsContext.IptExtracts.Any());
            Assert.True(_extractsContext.TempOtzExtracts.Any());
            Assert.True(_extractsContext.OtzExtracts.Any());
            Assert.True(_extractsContext.TempOvcExtracts.Any());
            Assert.True(_extractsContext.OvcExtracts.Any());

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

            Assert.False(_extractsContext.TempAllergiesChronicIllnessExtracts.Any());
            Assert.False(_extractsContext.AllergiesChronicIllnessExtracts.Any());
            Assert.False(_extractsContext.TempContactListingExtracts.Any());
            Assert.False(_extractsContext.ContactListingExtracts.Any());
            Assert.False(_extractsContext.DepressionScreeningExtracts.Any());
            Assert.False(_extractsContext.TempDepressionScreeningExtracts.Any());
            Assert.False(_extractsContext.TempDrugAlcoholScreeningExtracts.Any());
            Assert.False(_extractsContext.DrugAlcoholScreeningExtracts.Any());
            Assert.False(_extractsContext.TempEnhancedAdherenceCounsellingExtracts.Any());
            Assert.False(_extractsContext.EnhancedAdherenceCounsellingExtracts.Any());
            Assert.False(_extractsContext.TempGbvScreeningExtracts.Any());
            Assert.False(_extractsContext.GbvScreeningExtracts.Any());
            Assert.False(_extractsContext.TempIptExtracts.Any());
            Assert.False(_extractsContext.IptExtracts.Any());
            Assert.False(_extractsContext.TempOtzExtracts.Any());
            Assert.False(_extractsContext.OtzExtracts.Any());
            Assert.False(_extractsContext.TempOvcExtracts.Any());
            Assert.False(_extractsContext.OvcExtracts.Any());

            Assert.False(_extractsContext.ExtractHistory.Any(x => extractIds.Contains(x.ExtractId)));
        }

    }
}
