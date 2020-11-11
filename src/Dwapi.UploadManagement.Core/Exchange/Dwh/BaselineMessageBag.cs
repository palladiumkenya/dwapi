﻿using System;
using System.Collections.Generic;
using System.Linq;
 using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
 using Dwapi.SharedKernel.Enum;
 using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class BaselineMessageBag:IBaselinesMessageBag
    {
        private int stake = 10;
        public string EndPoint => "PatientBaselines";
        public IMessage<PatientBaselinesExtractView> Message { get; set; }
        public List<IMessage<PatientBaselinesExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "PatientBaselineExtract";
        public ExtractType ExtractType => ExtractType.PatientBaseline;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(PatientBaselinesExtract);
        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public BaselineMessageBag()
        {
        }
        public BaselineMessageBag(IMessage<PatientBaselinesExtractView> message)
        {
            Message = message;
        }
        public BaselineMessageBag(List<IMessage<PatientBaselinesExtractView>> messages)
        {
            Messages = messages;
        }
        public BaselineMessageBag(BaselineMessage message)
        {
            Message = message;
        }

        public static BaselineMessageBag Create(PatientExtractView patient)
        {
            var message = new BaselineMessage(patient);
            return new BaselineMessageBag(message);
        }

        public IMessageBag<PatientBaselinesExtractView> Generate(List<PatientBaselinesExtractView> extracts)
        {
            var messages = new List<IMessage<PatientBaselinesExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new BaselineMessage(artExtractView);
                messages.Add(message);
            }

            return new BaselineMessageBag(messages);
        }
        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
