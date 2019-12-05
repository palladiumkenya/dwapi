

using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SettingsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Dwapi.SettingsManagement.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class AppMetricRepositoryTests
    {
        private IAppMetricRepository _appMetricRepository;
        private DbContextOptions<SettingsContext> _options;
        private SettingsContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            _options = TestDbOptions.GetInMemoryOptions<SettingsContext>();
            var context = new SettingsContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.AddRange(TestData.GenerateAppMetrics());
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new SettingsContext(_options);
            _appMetricRepository = new AppMetricRepository(_context);
        }

        [Test]
        public void should_Load_current()
        {
            var mets = _appMetricRepository.GetAll().ToList();
            Assert.True(mets.Any());
        }
    }
}

class TestData
{
    public static List<AppMetric> GenerateAppMetrics()
    {
        var sql = "[\r\n  {\r\n    \"Id\": \"0db35d0f-a751-47f8-b1e3-ab1a009920c3\",\r\n    \"Version\": \"2.3.9\",\r\n    \"Name\": \"HivTestingService\",\r\n    \"LogDate\": \"2019-12-05T12:17:31.314734\",\r\n    \"LogValue\": \"{\\\"Name\\\":\\\"HivTestingService\\\",\\\"NoLoaded\\\":0,\\\"Version\\\":\\\"2.3.9\\\",\\\"ActionDate\\\":\\\"2019-12-05T12:17:31.289404+03:00\\\"}\",\r\n    \"Status\": 0\r\n  },\r\n  {\r\n    \"Id\": \"e28dcb18-6909-406e-896a-ab1a00998213\",\r\n    \"Version\": \"2.3.9\",\r\n    \"Name\": \"HivTestingService\",\r\n    \"LogDate\": \"2019-12-05T12:18:54.35538\",\r\n    \"LogValue\": \"{\\\"Name\\\":\\\"HivTestingService\\\",\\\"NoSent\\\":0,\\\"Version\\\":\\\"2.3.9\\\",\\\"ActionDate\\\":\\\"2019-12-05T12:18:54.35195+03:00\\\"}\",\r\n    \"Status\": 0\r\n  },\r\n  {\r\n    \"Id\": \"754b566f-a85a-47d4-838f-ab1a00999b6f\",\r\n    \"Version\": \"2.3.9\",\r\n    \"Name\": \"MasterPatientIndex\",\r\n    \"LogDate\": \"2019-12-05T12:19:15.995103\",\r\n    \"LogValue\": \"{\\\"Name\\\":\\\"MasterPatientIndex\\\",\\\"NoLoaded\\\":0,\\\"Version\\\":\\\"2.3.9\\\",\\\"ActionDate\\\":\\\"2019-12-05T12:19:15.994276+03:00\\\"}\",\r\n    \"Status\": 0\r\n  },\r\n  {\r\n    \"Id\": \"bf6042e6-b730-4e4e-93b3-ab1a009a2815\",\r\n    \"Version\": \"2.3.9\",\r\n    \"Name\": \"MasterPatientIndex\",\r\n    \"LogDate\": \"2019-12-05T12:21:16.014001\",\r\n    \"LogValue\": \"{\\\"Name\\\":\\\"MasterPatientIndex\\\",\\\"NoLoaded\\\":0,\\\"Version\\\":\\\"2.3.9\\\",\\\"ActionDate\\\":\\\"2019-12-05T12:21:16.013587+03:00\\\"}\",\r\n    \"Status\": 0\r\n  },\r\n  {\r\n    \"Id\": \"e8c2972d-f50a-4d80-a99d-ab1a009b27e6\",\r\n    \"Version\": \"2.3.9\",\r\n    \"Name\": \"CareTreatment\",\r\n    \"LogDate\": \"2019-12-05T12:24:54.312088\",\r\n    \"LogValue\": \"{\\\"Name\\\":\\\"CareTreatment\\\",\\\"NoLoaded\\\":0,\\\"Version\\\":\\\"2.3.9\\\",\\\"ActionDate\\\":\\\"2019-12-05T12:24:54.312023+03:00\\\"}\",\r\n    \"Status\": 0\r\n  },\r\n  {\r\n    \"Id\": \"19d34a2c-ec9d-4a45-a70e-ab1a009bb479\",\r\n    \"Version\": \"2.3.9\",\r\n    \"Name\": \"CareTreatment\",\r\n    \"LogDate\": \"2019-12-05T12:26:54.269072\",\r\n    \"LogValue\": \"{\\\"Name\\\":\\\"CareTreatment\\\",\\\"NoLoaded\\\":0,\\\"Version\\\":\\\"2.3.9\\\",\\\"ActionDate\\\":\\\"2019-12-05T12:26:54.268993+03:00\\\"}\",\r\n    \"Status\": 0\r\n  },\r\n  {\r\n    \"Id\": \"2059af7c-5bbd-4285-b600-ab1a009f5822\",\r\n    \"Version\": \"2.3.9\",\r\n    \"Name\": \"CareTreatment\",\r\n    \"LogDate\": \"2019-12-05T12:40:09.284106\",\r\n    \"LogValue\": \"{\\\"Name\\\":\\\"CareTreatment\\\",\\\"NoSent\\\":0,\\\"Version\\\":\\\"2.3.9\\\",\\\"ActionDate\\\":\\\"2019-12-05T12:40:09.283732+03:00\\\"}\",\r\n    \"Status\": 0\r\n  }\r\n]";
        var data= JsonConvert.DeserializeObject<List<AppMetric>>(sql);
        return data;
    }

}
