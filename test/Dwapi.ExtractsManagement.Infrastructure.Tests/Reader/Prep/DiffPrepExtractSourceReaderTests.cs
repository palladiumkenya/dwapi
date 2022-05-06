using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Infrastructure.Tests.TestArtifacts;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Infrastructure.Tests.Reader.Prep
{
    [TestFixture]
    [Category("PREPDiff")]
    public class DiffPrepExtractSourceReaderTests
    {
        private IDwhExtractSourceReader _reader;
        private List<Extract> _extracts;
        private DbProtocol _protocol;

        [OneTimeSetUp]
        public void Init()
        {
            TestInitializer.ClearDiffDb();
            if (!TestInitializer.LiveDb)
                TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrDiffConnectionString));
            else
                TestInitializer.SeedData(TestData.GenerateEmrSystems(TestInitializer.EmrDiffConnectionLiveString));
            _protocol = TestInitializer.Protocol;
            _extracts = TestInitializer.Extracts.Where(x => x.DocketId.IsSameAs("PREP")).ToList();
        }

        [SetUp]
        public void SetUp()
        {
            _reader = TestInitializer.ServiceProvider.GetService<IDwhExtractSourceReader>();
        }

        [TestCase(nameof(PatientPrepExtract))]
        [TestCase(nameof(PrepAdverseEventExtract))]
        [TestCase(nameof(PrepBehaviourRiskExtract))]
        [TestCase(nameof(PrepCareTerminationExtract))]
        [TestCase(nameof(PrepLabExtract))]
        [TestCase(nameof(PrepPharmacyExtract))]
        [TestCase(nameof(PrepVisitExtract))]
        public void should_Execute_Reader(string name)
        {
            var extract = _extracts.First(x => x.Name.IsSameAs(name));
            var reader = _reader.ExecuteReader(_protocol, extract).Result;
            Assert.NotNull(reader);
            reader.Read();
            Assert.NotNull(reader[0]);
            Assert.False((reader.GetDateTime(reader.GetOrdinal(nameof(PatientExtract.Date_Created))) as DateTime?).IsNullOrEmpty());
            Assert.False((reader.GetDateTime(reader.GetOrdinal(nameof(PatientExtract.Date_Last_Modified))) as DateTime?).IsNullOrEmpty());
            reader.Close();
        }
    }
}
