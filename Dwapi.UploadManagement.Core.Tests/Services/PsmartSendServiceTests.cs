using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.DTOs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Psmart;
using Dwapi.UploadManagement.Core.Services.Psmart;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Services
{
    [TestFixture]
    public class PsmartSendServiceTests
    {
        private readonly string url = "http://52.178.24.227:5757/api/inbound";

        private IPsmartSendService _psmartSendService;
        private List<PsmartStageDTO> _psmartStageDtos = new List<PsmartStageDTO>();

        [SetUp]
        public void SetUp()
        {
            _psmartSendService = new PsmartSendService();
            _psmartStageDtos = Builder<PsmartStageDTO>.CreateListOfSize(2).Build().ToList();
        }

        [Test]
        public void should_SendAsync()
        {
            var responses = _psmartSendService.SendAsync(url, _psmartStageDtos).Result;
            Assert.NotNull(responses);
            Console.WriteLine(responses);
        }
    }
}