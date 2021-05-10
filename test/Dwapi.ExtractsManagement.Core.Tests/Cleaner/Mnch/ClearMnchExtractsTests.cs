using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Cleaner.Mnch
{
    [TestFixture]
    public class ClearMnchExtractsTests
    {
        private IClearMnchExtracts _clearMnch;
        private List<Extract> _extracts;
        private ExtractsContext _extractsContext;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("MNCH")).ToList();
            //TestInitializer.LoadCt();
        }

        [SetUp]
        public void SetUp()
        {
            _clearMnch = TestInitializer.ServiceProvider.GetService<IClearMnchExtracts>();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [Test]
        public void should_clean()
        {
            var extractIds = _extracts.Select(x => x.Id).ToList();


            _clearMnch.Clear(extractIds).Wait();

            Assert.False(_extractsContext.TempPatientMnchExtracts.Any());
            Assert.False(_extractsContext.TempMnchEnrolmentExtracts.Any());
            Assert.False(_extractsContext.TempMnchArtExtracts.Any());
            Assert.False(_extractsContext.TempAncVisitExtracts.Any());
            Assert.False(_extractsContext.TempMatVisitExtracts.Any());
            Assert.False(_extractsContext.TempPncVisitExtracts.Any());
            Assert.False(_extractsContext.TempMotherBabyPairExtracts.Any());
            Assert.False(_extractsContext.TempCwcEnrolmentExtracts.Any());
            Assert.False(_extractsContext.TempCwcVisitExtracts.Any());
            Assert.False(_extractsContext.TempHeiExtracts.Any());
            Assert.False(_extractsContext.TempMnchLabExtracts.Any());

            Assert.False(_extractsContext.PatientMnchExtracts.Any());
            Assert.False(_extractsContext.MnchEnrolmentExtracts.Any());
            Assert.False(_extractsContext.MnchArtExtracts.Any());
            Assert.False(_extractsContext.AncVisitExtracts.Any());
            Assert.False(_extractsContext.MatVisitExtracts.Any());
            Assert.False(_extractsContext.PncVisitExtracts.Any());
            Assert.False(_extractsContext.MotherBabyPairExtracts.Any());
            Assert.False(_extractsContext.CwcEnrolmentExtracts.Any());
            Assert.False(_extractsContext.CwcVisitExtracts.Any());
            Assert.False(_extractsContext.HeiExtracts.Any());
            Assert.False(_extractsContext.MnchLabExtracts.Any());

            Assert.False(_extractsContext.ExtractHistory.Any(x => extractIds.Contains(x.ExtractId)));
        }

    }
}
