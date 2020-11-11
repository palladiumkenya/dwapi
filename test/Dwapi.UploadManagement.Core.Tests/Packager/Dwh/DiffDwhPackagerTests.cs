using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Packager.Dwh
{
    [TestFixture]
    [Category("DwhDiff")]
    public class DiffDwhPackagerTests
    {
        private IDwhPackager _packager;
        private Guid _pid;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDiffDb();
            var context = TestInitializer.ServiceProvider.GetService<UploadContext>();
            _pid = context.ClientPatientExtracts.AsNoTracking().First().Id;
        }

        [SetUp]
        public void SetUp()
        {
            _packager = TestInitializer.ServiceProvider.GetService<IDwhPackager>();
        }

        [Test]
        public void should_Generate_Diff_Manifest_With_Metrics()
        {
            var manifests = _packager.GenerateDiffWithMetrics(TestInitializer.IqEmrDto).ToList();
            Assert.False(manifests.Any(x=>x.UploadMode!=UploadMode.Differential));
            Assert.True(manifests.Any());
            Assert.True(manifests.Count==1);
            var m = manifests.First();
            Assert.True(m.PatientPks.Any());
            Assert.True(m.FacMetrics.Any(x => x.CargoType == CargoType.Metrics));
            Assert.True(m.FacMetrics.Any(x => x.CargoType == CargoType.AppMetrics));
            Log.Debug($"{m} [{m.UploadMode}]");
            m.FacMetrics.ForEach(c =>
            {
                Log.Debug($"{c.CargoType}");
                Log.Debug($"     {c.Metric} ");
            });
        }

        [Test]
        public void should_Generate_Diff_Art_Extracts_Initial()
        {
            var extracts =
                _packager.GenerateDiffBatchExtracts<PatientArtExtractView>(1, 50, "NDWH", nameof(PatientArtExtract));
            Assert.True(extracts.Any());
            foreach (var patientArtExtractView in extracts)
            {
                Log.Debug($"{patientArtExtractView.SiteCode}|{patientArtExtractView.PatientID}|{patientArtExtractView.Date_Created:yyyy MMMM dd}|{patientArtExtractView.Date_Last_Modified:yyyy MMMM dd}");
            }
        }

        [Test]
        public void should_Generate_Diff_Art_Extracts_Next()
        {
            InitNext();

            var extracts =
                _packager.GenerateDiffBatchExtracts<PatientArtExtractView>(1, 50, "NDWH", nameof(PatientArtExtract));
            Assert.True(extracts.Any());
            foreach (var patientArtExtractView in extracts)
            {
                Log.Debug($"{patientArtExtractView.SiteCode}|{patientArtExtractView.PatientID}|{patientArtExtractView.Date_Created:yyyy MMMM dd}|{patientArtExtractView.Date_Last_Modified:yyyy MMMM dd}");
            }
        }

        private void InitNext()
        {
            var context = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            var diffLog = context.DiffLogs.First(x => x.Docket == "NDWH" && x.Extract == nameof(PatientArtExtract));
            diffLog.LogLoad(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-2));
            diffLog.LogSent();
            diffLog.LastSent = DateTime.Now.AddDays(-1);

            var arts = context.PatientArtExtracts.Take(2).ToList();

            arts[0].Date_Created = DateTime.Now;
            arts[1].Date_Last_Modified = DateTime.Now;

            context.AttachRange(arts);
            context.SaveChanges();
        }
    }
}
