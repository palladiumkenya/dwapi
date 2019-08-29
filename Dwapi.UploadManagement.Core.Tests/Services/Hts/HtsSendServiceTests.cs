using System;
using System.Linq;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Exchange.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Dwapi.UploadManagement.Core.Packager.Hts;
using Dwapi.UploadManagement.Core.Services.Hts;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader;
using Dwapi.UploadManagement.Infrastructure.Reader.Hts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Services.Hts
{
    [TestFixture]
    [Category("Hts")]
    public class HtsSendServiceTests
    {
        private readonly string _authToken = @"1983aeda-1a96-30e9-adc0-fa7ae01bbebc";
        private readonly string _subId = "DWAPI";
        private readonly string url = "http://dwapicentral.westeurope.cloudapp.azure.com:7777";

        private IHtsSendService _htsSendService;
        private IServiceProvider _serviceProvider;
        private ManifestMessageBag _bag;
        private HtsMessageBag _clientBag;
        private CentralRegistry _registry;

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:DwapiConnection"];


            _serviceProvider = new ServiceCollection()
                .AddDbContext<UploadContext>(o => o.UseSqlServer(connectionString))
                .AddTransient<IHtsSendService,HtsSendService>()
            .AddTransient<IHtsPackager, HtsPackager>()
                .AddTransient<IEmrMetricReader, EmrMetricReader>()
                .AddTransient<IHtsExtractReader, HtsExtractReader>()

                .BuildServiceProvider();

            /*
                22704|TOGONYE DISPENSARY|KIRINYAGA
                22696|HERTLANDS MEDICAL CENTRE|NAROK
            */

            _bag = TestDataFactory.ManifestMessageBag(2,10001,10002);
            _clientBag = TestDataFactory.HtsMessageBag(5, 10001, 10002);
        }

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "HTS")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };
            _htsSendService = _serviceProvider.GetService<IHtsSendService>();
        }

        [Test]
        public void should_Send_Manifest()
        {
            var sendTo=new SendManifestPackageDTO(_registry);

            var responses = _htsSendService.SendManifestAsync(sendTo, _bag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x=>x.IsValid()).Any(x=>false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }


        [Test]
        public void should_Send_Clients()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _htsSendService.SendClientsAsync(sendTo, _clientBag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }

        [Test]
        public void should_Send_cLinkages()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _htsSendService.SendClientsLinkagesAsync(sendTo, _clientBag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }

        [Test]
        public void should_Send_ClientTest()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _htsSendService.SendClientTestsAsync(sendTo, _clientBag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }

        [Test]
        public void should_Send_TestKits()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _htsSendService.SendTestKitsAsync(sendTo, _clientBag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }

        [Test]
        public void should_Send_ClientTracing()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _htsSendService.SendClientTracingAsync(sendTo, _clientBag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }

        [Test]
        public void should_Send_PartnerTracing()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _htsSendService.SendPartnerTracingAsync(sendTo, _clientBag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }

        [Test]
        public void should_Send_PartnerNotificationServices()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _htsSendService.SendPartnerNotificationServicesAsync(sendTo, _clientBag).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }
    }
}
