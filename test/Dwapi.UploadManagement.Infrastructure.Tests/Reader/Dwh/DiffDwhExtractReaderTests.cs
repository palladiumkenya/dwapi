using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.ExtractsManagement.Infrastructure;
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
        private List<DiffLog> _diffLogs;

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
            var econtext= TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _diffLogs = econtext.DiffLogs.ToList();
        }

        [Test]
        public void should_Read_By_Predicate()
        {
            var diffLog = _diffLogs.First(x => x.Extract == nameof(PatientAdverseEventExtract));

            var extractViews = _reader.Read<PatientArtExtractView, Guid>(1, 2,
                x => x.Date_Created == diffLog.MaxCreated || x.Date_Last_Modified == diffLog.MaxModified).ToList();
            Assert.True(extractViews.Any());
            Assert.True(extractViews.Count == 2);
            Assert.NotNull(extractViews.First().PatientExtractView);
        }

    }
}
