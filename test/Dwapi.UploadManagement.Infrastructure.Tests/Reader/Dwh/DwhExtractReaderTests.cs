using System;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Infrastructure.Tests.Reader.Dwh
{
    [TestFixture]
    public class DwhExtractReaderTests
    {
        private IDwhExtractReader _reader;
        private UploadContext _context;

        [SetUp]
        public void SetUp()
        {
            _reader =TestInitializer.ServiceProvider.GetService<IDwhExtractReader>();
            _context= TestInitializer.ServiceProvider.GetService<UploadContext>();
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

        // [Test]
        public void should_Read_By_Ids()
        {
            var pid = _context.ClientPatientExtracts.AsNoTracking().First().Id;
            var extractViews = _reader.Read(pid);
            Assert.NotNull(extractViews);
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

        [Test]
        public void should_ART_ReadPaged()
        {
            var extractViews = _reader.Read<PatientArtExtractView,Guid>(1, 2).ToList();
            Assert.True(extractViews.Any());
            Assert.True(extractViews.Count==2);
            Assert.NotNull(extractViews.First().PatientExtractView);
        }

        [Test]
        public void should_ART_ReadAllPaged()
        {
            var extractViews = _reader.ReadSmart<PatientArtExtractView,Guid>(1, 2).ToList();
            Assert.True(extractViews.Any());
            Assert.True(extractViews.Count==2);
            Assert.Null(extractViews.First().PatientExtractView);
        }

        [Test]
        public void should_ART_Count()
        {
            var totalRecords = _reader.GetTotalRecords<PatientArtExtractView,Guid>();
            Assert.True(totalRecords>0);
        }
    }
}
