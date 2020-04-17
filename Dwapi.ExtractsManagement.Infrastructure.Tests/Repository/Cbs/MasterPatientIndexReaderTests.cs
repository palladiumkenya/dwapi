using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Infrastructure.Tests.TestArtifacts;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Repository.Cbs
{
    [TestFixture]
    [Category("Cbs")]
    public class MasterPatientIndexRepositoryTests
    {
        private IMasterPatientIndexRepository _repository;
        private ExtractsContext _context;
        private List<MasterPatientIndex> _indices;
        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            _indices = TestData.GenerateMpis();
            TestInitializer.SeedData<ExtractsContext>(_indices);
            _context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
        }

        [SetUp]
        public void SetUp()
        {
            _repository = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexRepository>();
        }


        [Test]
        public void should_GetViewL()
        {
            var mpis = _repository.GetView().ToList();
            Assert.True(mpis.Any());
        }

        [Test]
        public void shoul_Update_Sent_Items()
        {
            var mpiIds = _indices.Select(x => x.Id).ToList();
            var  sentItems = mpiIds.Select(x => new SentItem(x, SendStatus.Sent)).ToList();
            _repository.UpdateSendStatus(sentItems);

            _repository = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexRepository>();
            var updated = _repository.GetAll(x => x.Status == $"{nameof(SendStatus.Sent)}").ToList();
            Assert.True(updated.Count==2);
        }
    }
}
