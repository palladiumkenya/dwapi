using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Tests.TestArtifacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Repository.Cbs
{
    [TestFixture]
    [Category("Cbs")]
    public class TempMasterPatientIndexRepositoryTests
    {
        private ITempMasterPatientIndexRepository _repository;
        private ExtractsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrConnectionString));
            TestInitializer.SeedData<ExtractsContext>(
                TestData.GenerateData<TempMasterPatientIndex>(),
                TestData.GenerateMpis());
            _context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _repository = TestInitializer.ServiceProvider.GetService<ITempMasterPatientIndexRepository>();
        }

        [Test]
        public void should_Clear()
        {
            Assert.True(_context.TempMasterPatientIndices.AsNoTracking().Any());
            Assert.True(_context.MasterPatientIndices.AsNoTracking().Any());

            _repository.Clear().Wait();

            _context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            Assert.False(_context.TempMasterPatientIndices.AsNoTracking().Any());
            Assert.False(_context.MasterPatientIndices.AsNoTracking().Any());
        }

        [Test]
        public void should_BatchInsert()
        {
            var mpis = TestData.GenerateData<TempMasterPatientIndex>();
            var result = _repository.BatchInsert(mpis);
            Assert.True(result);
        }
    }
}
