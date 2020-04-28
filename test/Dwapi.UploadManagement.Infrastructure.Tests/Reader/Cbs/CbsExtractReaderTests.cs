using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Reader.Cbs
{
    [TestFixture]
    public class CbsExtractReaderTests
    {
        private ICbsExtractReader _reader;

        [SetUp]
        public void SetUp()
        {
            _reader =TestInitializer.ServiceProvider.GetService<ICbsExtractReader>();
        }

        [Test]
        public void should_ReadAll()
        {
            var mpis = _reader.ReadAll().ToList();
            Assert.True(mpis.Any());
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
