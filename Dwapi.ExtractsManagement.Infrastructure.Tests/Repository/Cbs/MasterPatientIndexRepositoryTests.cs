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
        private List<MasterPatientIndex> _patientIndices;
        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDb();
            _patientIndices = TestData.GenerateMpis();
            TestInitializer.SeedData(_patientIndices);
        }

        [SetUp]
        public void SetUp()
        {
            _repository = TestInitializer.ServiceProvider.GetService<IMasterPatientIndexRepository>();
        }

        [Test]
        public void should_GetView_MsSQL()
        {
            var patientIndices = _repository.GetView().ToList();
            Assert.True(patientIndices.Any());
        }

        [Test]
        public void should_Update_Sent_Items_MsSQL()
        {
            var mpiIds = _patientIndices.Select(x => x.Id).ToList();
            var  sentItems = mpiIds.Select(x => new SentItem(x, SendStatus.Sent)).ToList();
            _repository.UpdateSendStatus(sentItems);

            _repository=TestInitializer.ServiceProvider.GetService<IMasterPatientIndexRepository>();
            var updated = _repository.GetAll(x => x.Status == $"{nameof(SendStatus.Sent)}").ToList();
            Assert.True(updated.Count==2);
        }
    }
}
