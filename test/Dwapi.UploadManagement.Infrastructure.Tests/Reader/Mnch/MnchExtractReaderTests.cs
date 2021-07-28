using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mnch;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Reader.Mnch
{
    [TestFixture]
    public class MnchExtractReaderTests
    {
        private IMnchExtractReader _reader;

        [SetUp]
        public void SetUp()
        {
            _reader = TestInitializer.ServiceProvider.GetService<IMnchExtractReader>();
        }

        [Test]
        public void should_Read_PatientMnchs()
        {
            var profiles = _reader.ReadAllPatientMnchs().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_MnchEnrolments()
        {
            var profiles = _reader.ReadAllMnchEnrolments().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_MnchArts()
        {
            var profiles = _reader.ReadAllMnchArts().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_AncVisits()
        {
            var profiles = _reader.ReadAllAncVisits().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_MatVisits()
        {
            var profiles = _reader.ReadAllMatVisits().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_PncVisits()
        {
            var profiles = _reader.ReadAllPncVisits().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_MotherBabyPairs()
        {
            var profiles = _reader.ReadAllMotherBabyPairs().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_CwcEnrolments()
        {
            var profiles = _reader.ReadAllCwcEnrolments().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_CwcVisits()
        {
            var profiles = _reader.ReadAllCwcVisits().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_Heis()
        {
            var profiles = _reader.ReadAllHeis().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_Read_MnchLabs()
        {
            var profiles = _reader.ReadAllMnchLabs().ToList();
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
