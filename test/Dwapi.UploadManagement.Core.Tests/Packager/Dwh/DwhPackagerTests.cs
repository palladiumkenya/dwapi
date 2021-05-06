using System;
using System.Linq;
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
    [Category("Dwh")]
    public class DwhPackagerTests
    {
        private IDwhPackager _packager;
        private Guid _pid;

        [OneTimeSetUp]
        public void Init()
        {
            var context= TestInitializer.ServiceProvider.GetService<UploadContext>();
            _pid = context.ClientPatientExtracts.AsNoTracking().First().Id;
        }

        [SetUp]
        public void SetUp()
        {
            _packager = TestInitializer.ServiceProvider.GetService<IDwhPackager>();
        }

        [Test]
        public void should_Generate_Manifest()
        {
            var manifests = _packager.Generate(TestInitializer.IqEmrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count==1);
            var m = manifests.First();
            Assert.True(m.PatientPks.Any());
            Log.Debug($"{m}");
        }

        [Test]
        public void should_Generate_Multi_Manifest()
        {
            var emrDto = TestInitializer.IqEmrMultiDto;
            var manifests = _packager.Generate(emrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count>1);
            foreach (var m in manifests)
            {
                Assert.True(m.PatientPks.Any());
                Log.Debug($"{m}");
            }
        }

        [Test]
        public void should_Generate_Comm_Manifest()
        {
            var emrDto = TestInitializer.KeEmrCommDto;
            var manifests = _packager.Generate(emrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count>1);
            foreach (var m in manifests)
            {
                Assert.True(m.PatientPks.Any());
                Log.Debug($"{m}");
            }
        }

        [Test]
        public void should_Generate_Manifest_With_Metrics()
        {
            var manifests = _packager.GenerateWithMetrics(TestInitializer.IqEmrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count==1);
            var m = manifests.First();
            Assert.True(m.PatientPks.Any());
            Assert.True(m.FacMetrics.Any(x => x.CargoType == CargoType.Metrics));
            Assert.True(m.FacMetrics.Any(x => x.CargoType == CargoType.AppMetrics));
            Log.Debug($"{m}");
            m.FacMetrics.ForEach(c =>
            {
                Log.Debug($"{c.CargoType}");
                Log.Debug($"     {c.Metric} ");
            });
        }

        [Test]
        public void should_Generate_Multi_Manifest_With_Metrics()
        {
            var emrDto = TestInitializer.IqEmrMultiDto;
            var manifests = _packager.GenerateWithMetrics(emrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count > 1);
            foreach (var m in manifests)
            {
                Assert.True(m.PatientPks.Any());
                Assert.True(m.FacMetrics.Any(x => x.CargoType == CargoType.Metrics));
                Assert.True(m.FacMetrics.Any(x => x.CargoType == CargoType.AppMetrics));
                Log.Debug($"{m}");
                m.FacMetrics.ForEach(c =>
                {
                    Log.Debug($"{c.CargoType}");
                    Log.Debug($"     {c.Metric} ");
                });
            }
        }

        [Test]
        public void should_Generate_Comm_Manifest_With_Metrics()
        {
            var emrDto = TestInitializer.KeEmrCommDto;
            var manifests = _packager.GenerateWithMetrics(emrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count > 1);
            foreach (var m in manifests)
            {
                Assert.True(m.PatientPks.Any());
                Assert.True(m.FacMetrics.Any(x => x.CargoType == CargoType.Metrics));
                Assert.True(m.FacMetrics.Any(x => x.CargoType == CargoType.AppMetrics));
                Log.Debug($"{m}");
                m.FacMetrics.ForEach(c =>
                {
                    Log.Debug($"{c.CargoType}");
                    Log.Debug($"     {c.Metric} ");
                });
            }
        }


        [Test]
        public void should_Generate_Art_Extracts()
        {
            var extracts = _packager.GenerateBatchExtracts<PatientArtExtractView,Guid>(1,1);
            Assert.True(extracts.Any());
        }

        [Test]
        public void should_Get_Art_PackageInfo()
        {
            var extracts = _packager.GetPackageInfo<PatientArtExtractView,Guid>(1);
            Assert.True(extracts.PageCount>0);
            Assert.True(extracts.TotalRecords>0);
        }


    }
}
