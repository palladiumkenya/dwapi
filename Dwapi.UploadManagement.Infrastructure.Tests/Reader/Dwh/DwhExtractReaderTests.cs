using System;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Reader
{
    [TestFixture]
    public class DwhExtractReaderTests
    {
        private IDwhExtractReader _reader;
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
            _reader =TestInitializer.ServiceProvider.GetService<IDwhExtractReader>();
        }

        [Test]
        public void should_ReadProfiles()
        {
            var profiles = _reader.ReadProfiles().ToList();
            Assert.True(profiles.Any());
        }

        [Test]
        public void should_ReadAllIds()
        {
            var extractViews = _reader.ReadAllIds().ToList();
            Assert.True(extractViews.Any());
        }

        [Test]
        public void should_Read_By_Ids()
        {
            var extractViews = _reader.Read(_pid);
            Assert.NotNull(extractViews);
            Assert.True(extractViews.PatientArtExtracts.Any());
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
