using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Services;
using Dwapi.ExtractsManagement.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
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
        private Mock<HttpMessageHandler> _handlerMock;

        [SetUp]
        public void SetUp()
        {
            _authProtocol=new AuthProtocol();
            _authProtocol.Url = "https://kenyahmis.org/api";
            _url = "metrics";
            _emrMetricRepository = TestInitializer.ServiceProvider.GetService<IEmrMetricRepository>();
            _metricsService = TestInitializer.ServiceProvider.GetService<IEmrMetricsService>();

             _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"lastLoginDate\":\"2020-03-19T11:46:55.423\",\"emrVersion\":\"EMR Ver 3.0 R1 2\",\"emrName\":\"EMR\",\"lastMoH731RunDate\":\"2019-10-01T00:00:00\"}"),
                })
                .Verifiable();

            var httpClient = new HttpClient(_handlerMock.Object);
            _metricsService.Client = httpClient;
        }

        [Test]
        public void should_read_by_token()
        {
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
            _authProtocol.UserName = "user";
            _authProtocol.Password = "pass";

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

/*
    _authProtocol.Url = "https://auth.kenyahmis.org/IQCareSettingsApi/api";
    _url = "Matrix";

     _authProtocol.Url = "http://data.kenyahmis.org:7000/openmrs/ws/rest/v1/smartcard";
    _url = "getemrmetrics";
    _authProtocol.UserName = "admin";
    _authProtocol.Password = "Admin123";
*/
