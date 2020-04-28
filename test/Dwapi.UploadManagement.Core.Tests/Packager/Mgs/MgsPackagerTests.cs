using System;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mgs;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Packager.Mgs
{
    [TestFixture]
    [Category("Mgs")]
    public class MgsPackagerTests
    {
        private IMgsPackager _packager;

        [SetUp]
        public void Init()
        {
            _packager = TestInitializer.ServiceProvider.GetService<IMgsPackager>();
        }

        [Test]
        public void should_Generate_Manifest()
        {
            var manifests = _packager.Generate(EmrSetup.SingleFacility).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count==1);
            var m = manifests.First();
            Assert.True(m.Cargoes.Any());
            Log.Debug($"{m}");
        }

        [Test]
        public void should_Generate_Multi_Manifest()
        {
            var manifests = _packager.Generate(EmrSetup.MultiFacility).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count>1);
            foreach (var m in manifests)
            {
                Assert.True(m.Cargoes.Any());
                Log.Debug($"{m}");
            }
        }

        [Test]
        public void should_Generate_Manifest_With_Metrics()
        {
            var manifests = _packager.GenerateWithMetrics(EmrSetup.SingleFacility).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count == 1);
            var m = manifests.First();
            Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Migration));
            Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Metrics));
            Assert.True(m.Cargoes.Any(x => x.Type == CargoType.AppMetrics));
            Log.Debug($"{m}");
            m.Cargoes.ForEach(c =>
            {
                Log.Debug($"{c.Type}");
                Log.Debug($"     {c.Items} ");
            });
        }

        [Test]
        public void should_Generate_Multi_Manifest_With_Metrics()
        {
            var manfiests = _packager.GenerateWithMetrics(EmrSetup.MultiFacility).ToList();
            Assert.True(manfiests.Any());
            Assert.True(manfiests.Count>1);
            foreach (var m in manfiests)
            {
                Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Migration));
                Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Metrics));
                Assert.True(m.Cargoes.Any(x => x.Type == CargoType.AppMetrics));
                Log.Debug($"{m}");
                m.Cargoes.ForEach(c =>
                {
                    Log.Debug($"{c.Type}");
                    Log.Debug($"     {c.Items} ");
                });
            }
        }

        [Test]
        public void should_Generate_Migration_Metrics()
        {
            var manfiests = _packager.GenerateMigrations().ToList();
            Assert.True(manfiests.Any());
            var m = manfiests.First();
            Console.WriteLine($"{m} |{m.Metric}");
        }
    }
}
