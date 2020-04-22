using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Profiles.Hts;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Dwapi.UploadManagement.Core.Interfaces.Services.Cbs;
using Dwapi.UploadManagement.Core.Packager.Cbs;
using Dwapi.UploadManagement.Core.Profiles;
using Dwapi.UploadManagement.Core.Services;
using Dwapi.UploadManagement.Core.Services.Cbs;
using Dwapi.UploadManagement.Infrastructure.Data;
using Dwapi.UploadManagement.Infrastructure.Reader.Cbs;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Serilog;

namespace Dwapi.UploadManagement.Core.Tests.Services.Cbs
{
    [TestFixture]
    [Category("Cbs")]
    public class CbsSendServiceTests
    {
        private readonly string _authToken = Guid.NewGuid().ToString();
        private readonly string _subId = "DWAPI";
        private readonly string url = "https://kenyahmis.org/api";

        private ICbsSendService _cbsSendService;
        private CentralRegistry _registry;
        private Mock<HttpMessageHandler> _manifestHandlerMock,_mpiHandlerMock;

        [SetUp]
        public void SetUp()
        {
            _registry = new CentralRegistry(url, "CBS")
            {
                AuthToken = _authToken,
                SubscriberId = _subId
            };

            _cbsSendService =TestInitializer.ServiceProvider.GetService<ICbsSendService>();
            _manifestHandlerMock = MockHelpers.HttpHandler(new StringContent(""));
            _mpiHandlerMock = MockHelpers.HttpHandler(new StringContent(""));

        }

        [Test]
        public void should_Send_Manifest()
        {
            var httpClient = new HttpClient(_manifestHandlerMock.Object);
            _cbsSendService.Client = httpClient;
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _cbsSendService.SendManifestAsync(sendTo).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendManifestResponse => Log.Debug($"SENT! > {sendManifestResponse}"));
        }

        [Test]
        public void should_Send_Mpi()
        {
            _mpiHandlerMock = _manifestHandlerMock;
            _mpiHandlerMock..ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{"+$"\"{nameof(SendManifestResponse.FacilityKey)}\":\"{Guid.Empty}\""+"}"),
                })
                .Verifiable();
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _cbsSendService.SendMpiAsync(sendTo).Result;
            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            foreach (var sendManifestResponse in responses)
            {
                Console.WriteLine(sendManifestResponse);
            }
        }
     }
}
