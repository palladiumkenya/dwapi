using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Services.Psmart;
using Dwapi.UploadManagement.Core.Services.Psmart;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace Dwapi.UploadManagement.Core.Tests.Services
{
    [TestFixture]
    public class PsmartSendServiceTests
    {
        private readonly string _authToken = @"268DFA3EB92BC53FAE94A048E23112A1";
        private readonly string _subId = "DWAPI";
        private readonly string url = "http://52.178.24.227:8026";
        private Registry _registry;
        private SendPackageDTO _sendPackageDTO;

        private IPsmartSendService _psmartSendService;
        private List<PsmartStageDTO> _psmartStageDtos = new List<PsmartStageDTO>();
        private PsmartMessage _psmartMessage;
   

        [SetUp]
        public void SetUp()
        {
            _registry=new Registry(url);
            _registry.AuthToken = _authToken;
            _registry.SubscriberId = _subId;
            _psmartSendService = new PsmartSendService();
            _psmartStageDtos = Builder<PsmartStageDTO>.CreateListOfSize(2).Build().ToList();
            _sendPackageDTO = Builder<SendPackageDTO>.CreateNew().With(x=>x.Destination=_registry).Build();
            var pMessages = new List<string>{"message 1", "message 1" };
            _psmartMessage=new PsmartMessage(pMessages);
        }

        [Test]
        public void should_SendAsync()
        {
            var responses = _psmartSendService.SendAsync(_sendPackageDTO, _psmartStageDtos).Result;
            Assert.NotNull(responses);
            Console.WriteLine(responses);
        }
        [Test]
        public void should_SendToRegistryAsync()
        {

            var smartMessages = Builder<SmartMessage>.CreateListOfSize(10).Build().ToList();
            var bag=new PsmartBag(smartMessages);

            var responses = _psmartSendService.SendAsync(_sendPackageDTO, bag).Result;
            Assert.NotNull(responses);
            Console.WriteLine(responses);
        }
        [Test]
        public void should_SendToRegistryAsync_MUltiple()
        {

            var smartMessages = Builder<SmartMessage>.CreateListOfSize(10).Build().ToList();
            var bag2 = new PsmartBag(smartMessages.Take(5).ToList());
            var bag = new PsmartBag(smartMessages.TakeLast(5).ToList());
                        var responses = _psmartSendService.SendAsync(_sendPackageDTO, new List<PsmartBag>{bag2,bag}).Result;
            Assert.NotNull(responses);
            Console.WriteLine(responses);
        }
    }
}