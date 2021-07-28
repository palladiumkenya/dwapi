using System.Linq;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Data
{
    [TestFixture]
    public class UploadContextTests
    {
        private UploadContext _context;


        [OneTimeSetUp]
        public void Init()
        {
            _context = TestInitializer.ServiceProvider.GetService<UploadContext>();
        }


        [Test]
        public void should_load_MPI_From_References()
        {
            Assert.True(_context.ClientMasterPatientIndices.Any());
        }

        [Test]
        public void should_load_CT_From_References()
        {
            Assert.True(_context.ClientPatientExtracts.Any());
            Assert.True(_context.ClientPatientArtExtracts.Any());
            Assert.True(_context.ClientPatientBaselinesExtracts.Any());
            Assert.True(_context.ClientPatientLaboratoryExtracts.Any());
            Assert.True(_context.ClientPatientPharmacyExtracts.Any());
            Assert.True(_context.ClientPatientStatusExtracts.Any());
            Assert.True(_context.ClientPatientVisitExtracts.Any());
            Assert.True(_context.ClientPatientAdverseEventExtracts.Any());
        }

        [Test]
        public void should_load_HTS_From_References()
        {
            Assert.True(_context.ClientPatientExtracts.Any());
            Assert.True(_context.ClientPatientArtExtracts.Any());
            Assert.True(_context.ClientPatientBaselinesExtracts.Any());
            Assert.True(_context.ClientPatientLaboratoryExtracts.Any());
            Assert.True(_context.ClientPatientPharmacyExtracts.Any());
            Assert.True(_context.ClientPatientStatusExtracts.Any());
            Assert.True(_context.ClientPatientVisitExtracts.Any());
            Assert.True(_context.ClientPatientAdverseEventExtracts.Any());
        }

        [Test]
        public void should_load_Migration_Metrics_From_References()
        {
            Assert.True(_context.MetricMigrationExtracts.Any());
        }

        [Test]
        public void should_load_Metrics_From_References()
        {
            Assert.True(_context.AppMetrics.Any());
            Assert.True(_context.EmrMetrics.Any());
        }

        [Test]
        public void should_load_DiffLogs_From_References()
        {
            Assert.True(_context.DiffLogs.Any());
        }


        [Test]
        public void should_load_MNCH_From_References()
        {
            Assert.True(_context.ClientPatientMnchExtracts.Any());
            Assert.True(_context.ClientMnchEnrolmentExtracts.Any());
            Assert.True(_context.ClientMnchArtExtracts.Any());
            Assert.True(_context.ClientAncVisitExtracts.Any());
            Assert.True(_context.ClientMatVisitExtracts.Any());
            Assert.True(_context.ClientPncVisitExtracts.Any());
            Assert.True(_context.ClientMotherBabyPairExtracts.Any());
            Assert.True(_context.ClientCwcEnrolmentExtracts.Any());
            Assert.True(_context.ClientCwcVisitExtracts.Any());
            Assert.True(_context.ClientHeiExtracts.Any());
            Assert.True(_context.ClientMnchLabExtracts.Any());

        }
    }
}
