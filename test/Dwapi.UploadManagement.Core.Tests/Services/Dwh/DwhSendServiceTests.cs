using System;
using System.Linq;
using System.Net.Http;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Services.Dwh
{
    [TestFixture]
    [Category("Dwh")]
    public class DwhSendServiceTests
    {
        private readonly string _authToken = Guid.NewGuid().ToString();
        private readonly string _subId = "DWAPI";
        private readonly string url = "https://kenyahmis.org/api";

        private IDwhSendService _sendService;
        private CentralRegistry _registry;
        private Mock<HttpMessageHandler> _manifestHandlerMock,_handlerMock;

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "NDWH")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };
            _sendService =TestInitializer.ServiceProvider.GetService<IDwhSendService>();
            _manifestHandlerMock = MockHelpers.HttpHandler(new StringContent("{"+$"\"{nameof(SendDhwManifestResponse.MasterFacility)}\":\"Demo Maun Facilty\""+"}"));
            _handlerMock = MockHelpers.HttpHandler(new StringContent("{"+$"\"Response\":\"{Guid.Empty}\""+"}"));
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

        public void should_Send_Extracts()
        {
            _sendService.Client = new HttpClient(_handlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendExtractsAsync(sendTo).Result;

            Assert.NotNull(responses);
            Assert.True(responses.Any());
            responses.ForEach(sendResponse => Log.Debug($"SENT! > {sendResponse}"));
        }

    }
}
