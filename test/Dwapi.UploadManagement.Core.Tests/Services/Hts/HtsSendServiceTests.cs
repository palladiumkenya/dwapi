using System;
using System.Linq;
using System.Net.Http;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Hts;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Services.Hts
{
    [TestFixture]
    [Category("Hts")]
    public class HtsSendServiceTests
    {
        private readonly string _authToken = Guid.NewGuid().ToString();
        private readonly string _subId = "DWAPI";
        private readonly string url = "https://kenyahmis.org/api";

        private IHtsSendService _sendService;
        private CentralRegistry _registry;
        private Mock<HttpMessageHandler> _manifestHandlerMock,_handlerMock;

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "HTS")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };
            _sendService =TestInitializer.ServiceProvider.GetService<IHtsSendService>();
            _manifestHandlerMock = MockHelpers.HttpHandler(new StringContent("{"+$"\"{nameof(SendManifestResponse.FacilityKey)}\":\"{Guid.Empty}\""+"}"));
            _handlerMock = MockHelpers.HttpHandler(new StringContent("{"+$"\"{nameof(SendMpiResponse.BatchKey)}\":\"{Guid.Empty}\""+"}"));
        }

        [Test]
        public void should_Send_Manifest()
        {
            _sendService.Client = new HttpClient(_manifestHandlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendManifestAsync(sendTo,"v1").Result;

            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendManifestResponse => Log.Debug($"SENT! > {sendManifestResponse}"));
        }


        [Test]
        public void should_Send_Clients()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendClientsLinkagesAsync(sendTo).Result;

            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendMpiResponse => Log.Debug($"SENT! > {sendMpiResponse}"));
        }

        [Test]
        public void should_Send_cLinkages()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendClientsLinkagesAsync(sendTo).Result;
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
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendClientTestsAsync(sendTo).Result;
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
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendTestKitsAsync(sendTo).Result;
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
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendClientTracingAsync(sendTo).Result;
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
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendPartnerTracingAsync(sendTo).Result;
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
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendPartnerNotificationServicesAsync(sendTo).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }
    }
}
