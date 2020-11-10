using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.ExtractsManagement.Core.Tests.TestArtifacts;
using Dwapi.ExtractsManagement.Infrastructure;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Application.Events
{
    [TestFixture]
    public class DocketExtractSentTests
    {
        private IPatientAdverseEventLoader _loader;

        private List<Extract> _extracts;
        private DbProtocol _protocol;
        private ExtractsContext _extractsContext;
        private Extract _extract;
        private int _count;
        private IMediator _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDiffDb();
            TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrDiffConnectionString));
            InitExtractor();
        }

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.ServiceProvider.GetService<IMediator>();
            SetupExtractor();
        }

        [Test]
        public void should_Log_Send()
        {
            var count = _loader.Load(_extract.Id, _count).Result;
            Assert.True(count > 0);

            _mediator.Publish(new DocketExtractSent("NDWH", nameof(PatientAdverseEventExtract))).Wait();

            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();

            var sql=$"select * from {nameof(DiffLog)}s where {nameof(DiffLog.Docket)}=@Docket and {nameof(DiffLog.Extract)}=@Extract";

            var extractDiffLog = _extractsContext.Database.GetDbConnection()
                .QueryFirst<DiffLog>(sql, new {Docket = "NDWH", Extract = $"{nameof(PatientAdverseEventExtract)}"});

            Assert.AreEqual(extractDiffLog.LastCreated,extractDiffLog.MaxCreated);
            Assert.AreEqual(extractDiffLog.LastModified,extractDiffLog.MaxModified);
            Assert.False(extractDiffLog.LastSent.IsNullOrEmpty());

        }

        private void InitExtractor()
        {
            _protocol = TestInitializer.Protocol;
            _extracts = TestInitializer.Extracts.Where(x => Extentions.IsSameAs(x.DocketId, "NDWH")).ToList();
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();

            var patientExtract = _extracts.First(x => x.Name.IsSameAs(nameof(PatientExtract)));
            var patientLoader = TestInitializer.ServiceProvider.GetService<IPatientLoader>();
            var patientSourceExtractor = TestInitializer.ServiceProvider.GetService<IPatientSourceExtractor>();
            var tempCount = patientSourceExtractor.Extract(patientExtract, _protocol).Result;
            var patientCount = patientLoader.Load(patientExtract.Id, tempCount).Result;
        }

        private void SetupExtractor()
        {
            _loader = TestInitializer.ServiceProvider.GetService<IPatientAdverseEventLoader>();
            var _extractor = TestInitializer.ServiceProvider.GetService<IPatientAdverseEventSourceExtractor>();
            _extract = _extracts.First(x => x.Name.IsSameAs(nameof(PatientAdverseEventExtract)));
            _count = _extractor.Extract(_extract, _protocol).Result;
            _extractsContext = TestInitializer.ServiceProvider.GetService<ExtractsContext>();
            _extractsContext.DiffLogs.RemoveRange(_extractsContext.DiffLogs.Where(x=>x.Extract!=$"{nameof(PatientExtract)}"&&x.Docket=="NDWH"));
            _extractsContext.SaveChanges();
        }
    }
}
