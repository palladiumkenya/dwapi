using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Cleaner.Hts
{
    [TestFixture]
    public class ClearHtsExtractsTests
    {
        private IClearHtsExtracts _clearHts;
        private List<Extract> _extracts;
        private ExtractsContext _extractsContext;


        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("HTS")).ToList();
            TestInitializer.LoadHts();
        }

        [SetUp]
        public void SetUp()
        {
            _clearHts = TestInitializer.ServiceProvider.GetService<IClearHtsExtracts>();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [Test]
        public void should_clean()
        {
            var extractIds = _extracts.Select(x => x.Id).ToList();

            Assert.True(_extractsContext.TempHtsClientTestsExtracts.Any());
            Assert.True(_extractsContext.TempHtsTestKitsExtracts.Any());
            Assert.True(_extractsContext.TempHtsClientsLinkageExtracts.Any());
            Assert.True(_extractsContext.TempHtsPartnerTracingExtracts.Any());
            Assert.True(_extractsContext.TempHtsClientTracingExtracts.Any());
            Assert.True(_extractsContext.TempHtsPartnerNotificationServicesExtracts.Any());
            Assert.True(_extractsContext.TempHtsClientsExtracts.Any());
            Assert.True(_extractsContext.TempHtsEligibilityExtracts.Any());
            Assert.True(_extractsContext.TempHtsRiskScoresExtracts.Any());

            
            Assert.True(_extractsContext.HtsClientTestsExtracts.Any());
            Assert.True(_extractsContext.HtsTestKitsExtracts.Any());
            Assert.True(_extractsContext.HtsClientsLinkageExtracts.Any());
            Assert.True(_extractsContext.HtsPartnerTracingExtracts.Any());
            Assert.True(_extractsContext.HtsClientTracingExtracts.Any());
            Assert.True(_extractsContext.HtsPartnerNotificationServicesExtracts.Any());
            Assert.True(_extractsContext.HtsClientsExtracts.Any());
            Assert.True(_extractsContext.HtsEligibilityExtracts.Any());
            Assert.True(_extractsContext.HtsRiskScoresExtracts.Any());


            _clearHts.Clear(extractIds).Wait();

            Assert.False(_extractsContext.TempHtsClientTestsExtracts.Any());
            Assert.False(_extractsContext.TempHtsTestKitsExtracts.Any());
            Assert.False(_extractsContext.TempHtsClientsLinkageExtracts.Any());
            Assert.False(_extractsContext.TempHtsPartnerTracingExtracts.Any());
            Assert.False(_extractsContext.TempHtsClientTracingExtracts.Any());
            Assert.False(_extractsContext.TempHtsPartnerNotificationServicesExtracts.Any());
            Assert.False(_extractsContext.TempHtsClientsExtracts.Any());
            Assert.False(_extractsContext.TempHtsEligibilityExtracts.Any());
            Assert.False(_extractsContext.TempHtsRiskScoresExtracts.Any());


            Assert.False(_extractsContext.HtsClientTestsExtracts.Any());
            Assert.False(_extractsContext.HtsTestKitsExtracts.Any());
            Assert.False(_extractsContext.HtsClientsLinkageExtracts.Any());
            Assert.False(_extractsContext.HtsPartnerTracingExtracts.Any());
            Assert.False(_extractsContext.HtsClientTracingExtracts.Any());
            Assert.False(_extractsContext.HtsPartnerNotificationServicesExtracts.Any());
            Assert.False(_extractsContext.HtsClientsExtracts.Any());
            Assert.False(_extractsContext.TempHtsEligibilityExtracts.Any());
            Assert.False(_extractsContext.TempHtsRiskScoresExtracts.Any());

            Assert.False(_extractsContext.ExtractHistory.Any(x => extractIds.Contains(x.ExtractId)));
        }

    }
}
