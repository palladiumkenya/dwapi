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

        private ICbsSendService _sendService;
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
            _sendService =TestInitializer.ServiceProvider.GetService<ICbsSendService>();
            _manifestHandlerMock = MockHelpers.HttpHandler(new StringContent("{"+$"\"{nameof(SendManifestResponse.FacilityKey)}\":\"{Guid.Empty}\""+"}"));
            _mpiHandlerMock = MockHelpers.HttpHandler(new StringContent("{"+$"\"{nameof(SendMpiResponse.BatchKey)}\":\"{Guid.Empty}\""+"}"));
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
        public void should_Send_Mpi()
        {
            _sendService.Client = new HttpClient(_mpiHandlerMock.Object);
            var sendTo = new SendManifestPackageDTO(_registry);

            var responses = _sendService.SendMpiAsync(sendTo).Result;

            Assert.NotNull(responses);
            Assert.False(responses.Select(x => x.IsValid()).Any(x => false));
            responses.ForEach(sendMpiResponse => Log.Debug($"SENT! > {sendMpiResponse}"));
        }
     }
}
