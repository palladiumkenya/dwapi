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
    public class DiffDwhExtractReaderTests
    {
        private IDwhExtractReader _reader;
        private UploadContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDiffDb();
        }

        [SetUp]
        public void SetUp()
        {
            _reader =TestInitializer.ServiceProvider.GetService<IDwhExtractReader>();
            _context= TestInitializer.ServiceProvider.GetService<UploadContext>();
        }

        [Test]
        public void should_Read_Diff_ART_Initial()
        {
            var extractViews = _reader.Read<PatientArtExtractView,Guid>(1, 2).ToList();
            Assert.True(extractViews.Any());
            Assert.True(extractViews.Count==2);
            Assert.NotNull(extractViews.First().PatientExtractView);
        }
        [Test]
        public void should_Read_Diff_ART_Next()
        {
            var extractViews = _reader.Read<PatientArtExtractView,Guid>(1, 2).ToList();
            Assert.True(extractViews.Any());
            Assert.True(extractViews.Count==2);
            Assert.NotNull(extractViews.First().PatientExtractView);
        }
    }
}
