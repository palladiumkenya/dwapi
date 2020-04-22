using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Reader
{
    [TestFixture]
    public class HtsExtractReaderTests
    {
        private IHtsExtractReader _reader;

        [SetUp]
        public void SetUp()
        {
            _reader =TestInitializer.ServiceProvider.GetService<IHtsExtractReader>();
        }

        [Test]
        public void should_Read_Clients()
        {
            var profiles = _reader.ReadAllClients().ToList();
            Assert.True(profiles.Any());
        }
        [Test]
        public void should_Read_Client_Tests()
        {
            var profiles = _reader.ReadAllClientTests().ToList();
            Assert.True(profiles.Any());
        }
        [Test]
        public void should_Read_Kits()
        {
            var profiles = _reader.ReadAllTestKits().ToList();
            Assert.True(profiles.Any());
        }
        [Test]
        public void should_Read_Client_Tracings()
        {
            var profiles = _reader.ReadAllClientTracing().ToList();
            Assert.True(profiles.Any());
        }
        [Test]
        public void should_Read_Partner_Tracings()
        {
            var profiles = _reader.ReadAllPartnerTracing().ToList();
            Assert.True(profiles.Any());
        }
        [Test]
        public void should_Read_Pns()
        {
            var profiles = _reader.ReadAllPartnerNotificationServices().ToList();
            Assert.True(profiles.Any());
        }
        [Test]
        public void should_Read_Client_Linkages()
        {
            var profiles = _reader.ReadAllClientsLinkage().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_GetSites()
        {
            var sites = _reader.GetSites().ToList();
            Assert.True(sites.Any());
            sites.ForEach(site => Log.Debug($"{site}"));
        }

        [Test]
        public void should_GetSitePatientProfiles()
        {
            var sitePatientProfiles = _reader.GetSitePatientProfiles().ToList();
            Assert.True(sitePatientProfiles.Any());
            sitePatientProfiles.ForEach(site => Log.Debug($"{site}"));
        }
    }
}
