using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Services
{
    [TestFixture]
    public class EmrMetricsServiceTests
    {
        private IEmrMetricsService _metricsService;
        private AuthProtocol _authProtocol;
        private string _url;
        private IEmrMetricRepository _emrMetricRepository;

        [SetUp]
        public void SetUp()
        {
            _authProtocol=new AuthProtocol();
            _emrMetricRepository = TestInitializer.ServiceProvider.GetService<IEmrMetricRepository>();
            _metricsService = TestInitializer.ServiceProvider.GetService<IEmrMetricsService>();
        }

        [Test]
        public void should_read_by_token()
        {
            _authProtocol.Url = "https://auth.kenyahmis.org/IQCareSettingsApi/api";
            _url = "Matrix";

            var metrics = _metricsService.Read(_authProtocol, _url).Result;
            var savedMetrics = _emrMetricRepository.GetAll().First();

            Assert.NotNull(metrics);

            Assert.NotNull(savedMetrics);

            Assert.AreEqual(metrics.EmrName,savedMetrics.EmrName);
            Assert.AreEqual(metrics.EmrVersion    ,savedMetrics.EmrVersion);

            Console.WriteLine(metrics);
        }

        [Test]
        public void should_read_by_basic()
        {
            _authProtocol.Url = "http://data.kenyahmis.org:7000/openmrs/ws/rest/v1/smartcard";
            _url = "getemrmetrics";
            _authProtocol.UserName = "admin";
            _authProtocol.Password = "Admin123";

            var metrics = _metricsService.Read(_authProtocol, _url).Result;


            var savedMetrics = _emrMetricRepository.GetAll().First();

            Assert.NotNull(metrics);

            Assert.NotNull(savedMetrics);

            Assert.AreEqual(metrics.EmrName,savedMetrics.EmrName);
            Assert.AreEqual(metrics.EmrVersion    ,savedMetrics.EmrVersion);

            Console.WriteLine(metrics);
        }
    }
}
