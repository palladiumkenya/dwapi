using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Infrastructure.Tests.TestArtifacts;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Repository.Diff
{
    [TestFixture]
    public class DiffLogRepositoryTests
    {
        private IDiffLogRepository _repository;
        private List<DiffLog> _diffLogs;
        private ExtractsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDiffDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrDiffConnectionString));
            _diffLogs = TestData.GenerateDiffs();
            TestInitializer.SeedData<ExtractsContext>(_diffLogs);
            TestInitializer.SeedData<ExtractsContext>(
                TestData.GenerateData<PatientExtract>(),TestData.GenerateData<PatientAdverseEventExtract>());
            _context=TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _repository = TestInitializer.ServiceProvider.GetService<IDiffLogRepository>();
        }

        [Test]
        public void should_Get_Log()
        {
            var diffLog = _repository.GetLog("ndwh", "PatientExtract");
            Assert.NotNull(diffLog);
        }

        [Test]
        public void should_Init_Log_New()
        {
            var diffLog = _repository.InitLog("NDWH", "PatientStatusExtract");
            Assert.NotNull(diffLog);
        }

        [Test]
        public void should_Init_Log_Exisiting()
        {
            var diffLog = _repository.InitLog("ndwh", "PatientExtract");
            Assert.NotNull(diffLog);
        }

        [Test]
        public void should_Save_Log_New()
        {
            var diffLog = DiffLog.Create("NDWH", "PatientVisitExtract");
            _repository.SaveLog(diffLog);


            var context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            var savedDiffLog = context.DiffLogs.Find(diffLog.Id);

            Assert.NotNull(savedDiffLog);
        }

        [Test]
        public void should_Save_Log_Update()
        {

            var diffLog = _diffLogs.First(x => x.Extract == "PatientExtract");
            diffLog.LogSent();

            _repository.SaveLog(diffLog);


            var context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            var savedDiffLog = context.DiffLogs.Find(diffLog.Id);

            Assert.NotNull(savedDiffLog);
        }

        [TestCase("PatientAdverseEventExtracts")]
        [TestCase("PatientExtracts")]
        public void should_Generate_Diff(string extract)
        {
            var diffLog = _repository.GenerateDiff("NDWH", extract);
            Assert.NotNull(diffLog);
            Assert.False(diffLog.MaxCreated.IsNullOrEmpty());
            Assert.False(diffLog.MaxModified.IsNullOrEmpty());
        }
    }
}
