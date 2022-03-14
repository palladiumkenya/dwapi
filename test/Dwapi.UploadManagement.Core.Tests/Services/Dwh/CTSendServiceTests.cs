using System;
using System.Linq;
using System.Net.Http;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Exchange.Dwh.Smart;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Services.Dwh
{
    [TestFixture]
    [Category("Dwh")]
    public class CTSendServiceTests
    {
        private readonly string _authToken = Guid.NewGuid().ToString();
        private readonly string _subId = "DWAPI";
        private readonly string url = "http://localhost:21751";

        private ICTSendService _sendService;
        private CentralRegistry _registry;
        private Mock<HttpMessageHandler> _manifestHandlerMock, _handlerMock;

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "NDWH")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };
            _sendService = TestInitializer.ServiceProvider.GetService<ICTSendService>();
            _manifestHandlerMock = MockHelpers.HttpHandler(new StringContent(
                "{" + $"\"{nameof(SendDhwManifestResponse.MasterFacility)}\":\"Demo Maun Facilty\"" + "}"));
            _handlerMock = MockHelpers.HttpHandler(new StringContent("{" + $"\"BatchKey\":[\"{Guid.Empty}\"]" + "}"));
        }

        [Test]
        public void should_Send_Manifest()
        {
            _sendService.Client = new HttpClient(_manifestHandlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendManifestAsync(sendTo).Result;

            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendManifestResponse => Log.Debug($"SENT! > {sendManifestResponse}"));
        }

        [Test]
        public void should_Send_Smart_Manifest()
        {
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendSmartManifestAsync(sendTo,"2.8.0.0","3").Result;

            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendManifestResponse => Log.Debug($"SENT! > {sendManifestResponse}"));
        }


        [Test, Order(1)]
        public void should_Send_Smart_Patient()
        {
            var sendTo = new SendManifestPackageDTO(_registry);
            var manifestResponses = _sendService.SendSmartManifestAsync(sendTo,"2.8.0.0","3").Result;

            var responses = _sendService.SendSmartBatchExtractsAsync(sendTo, 2000, new PatientMessageSourceBag()).Result;
            Assert.NotNull(manifestResponses);
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }

        [Test, Order(2)]
        public void should_Send_Smart_Art()
        {
            var sendTo = new SendManifestPackageDTO(_registry);
            var manifestResponses = _sendService.SendSmartManifestAsync(sendTo,"2.8.0.0","3").Result;
            var mainResponses = _sendService.SendSmartBatchExtractsAsync(sendTo, 2000, new PatientMessageSourceBag()).Result;

            var responses = _sendService.SendSmartBatchExtractsAsync(sendTo, 2000, new ArtMessageSourceBag()).Result;
            Assert.NotNull(manifestResponses);
            Assert.NotNull(mainResponses);
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }

        [Test, Order(3)]
        public void should_Send_ART()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendBatchExtractsAsync(sendTo, 200, new ArtMessageBag()).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }

        [Test, Order(4)]
        public void should_Send_AdverseEvent()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendBatchExtractsAsync(sendTo, 200, new AdverseEventsMessageBag()).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }

        [Test, Order(4)]
        public void should_Send_Baseline()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendBatchExtractsAsync(sendTo, 200, new BaselineMessageBag()).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }

        [Test, Order(4)]
        public void should_Send_Lab()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendBatchExtractsAsync(sendTo, 200, new LabMessageBag()).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }

        [Test, Order(4)]
        public void should_Send_Pharmacy()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendBatchExtractsAsync(sendTo, 200, new PharmacyMessageBag()).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }

        [Test, Order(4)]
        public void should_Send_Status()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendBatchExtractsAsync(sendTo, 200, new StatusMessageBag()).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }

        [Test, Order(4)]
        public void should_Send_Visit()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendBatchExtractsAsync(sendTo, 200, new VisitsMessageBag()).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
        }
    }
}
