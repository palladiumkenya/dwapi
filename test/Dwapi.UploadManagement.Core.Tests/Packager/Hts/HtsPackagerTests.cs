using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Packager.Hts
{
    [TestFixture]
    [Category("Hts")]
    public class HtsPackagerTests
    {
        private IHtsPackager _packager;

        [SetUp]
        public void SetUp()
        {
            _packager = TestInitializer.ServiceProvider.GetService<IHtsPackager>();
        }

        [Test]
        public void should_Generate_Manifest()
        {
            var manifests = _packager.Generate(TestInitializer.IqEmrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count==1);
            var m = manifests.First();
            Assert.True(m.Cargoes.Any());
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
                Assert.True(m.Cargoes.Any());
                Log.Debug($"{m}");
            }
        }

        [Test]
        public void should_Generate_Manifest_With_Metrics()
        {
            var manifests = _packager.GenerateWithMetrics(TestInitializer.IqEmrDto).ToList();
            Assert.True(manifests.Any());
            Assert.True(manifests.Count == 1);
            var m = manifests.First();
            Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Patient));
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
            var emrDto = TestInitializer.IqEmrMultiDto;
            var manfiests = _packager.GenerateWithMetrics(emrDto).ToList();
            Assert.True(manfiests.Any());
            Assert.True(manfiests.Count>1);
            foreach (var m in manfiests)
            {
                Assert.True(m.Cargoes.Any(x => x.Type == CargoType.Patient));
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
        public void should_Generate_Clients()
        {
            var clients = _packager.GenerateClients();
            Assert.True(clients.Any());
        }

        [Test]
        public void should_Generate_ClientTests()
        {
            var tests = _packager.GenerateClientTests();
            Assert.True(tests.Any());
        }

        [Test]
        public void should_Generate_TestKits()
        {
            var kits = _packager.GenerateTestKits();
            Assert.True(kits.Any());
        }

        [Test]
        public void should_Generate_ClientTracings()
        {
            var clientTracing = _packager.GenerateClientTracing();
            Assert.True(clientTracing.Any());
        }

        [Test]
        public void should_Generate_PartnerTracing()
        {
            var tracing = _packager.GeneratePartnerTracing();
            Assert.True(tracing.Any());

        }
        [Test]
        public void should_Generate_PNS()
        {
            var partnerNotificationServices = _packager.GeneratePartnerNotificationServices();
            Assert.True(partnerNotificationServices.Any());

        }

        [Test]
        public void should_Generate_Linkage()
        {
            var linkages = _packager.GenerateClientLinkage();
            Assert.True(linkages.Any());

        }

    }
}
