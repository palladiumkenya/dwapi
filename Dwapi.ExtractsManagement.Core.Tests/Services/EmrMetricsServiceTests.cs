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
            _authProtocol.Url = "https://palladiumkenya.github.io/dwapi/stuff";
            _url = "metrics.Json";
            _emrMetricRepository = TestInitializer.ServiceProvider.GetService<IEmrMetricRepository>();
            _metricsService = TestInitializer.ServiceProvider.GetService<IEmrMetricsService>();
        }

        [Test]
        public void should_read()
        {
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