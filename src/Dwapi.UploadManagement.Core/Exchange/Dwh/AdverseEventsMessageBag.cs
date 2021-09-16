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
    public class AdverseEventsMessageBag:IAdverseEventsMessageBag
    {
        private int stake = 5;
        public string EndPoint => "PatientAdverseEvents";
        public IMessage<PatientAdverseEventView> Message { get; set; }
        public List<IMessage<PatientAdverseEventView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "PatientAdverseEventExtract";
        public ExtractType ExtractType => ExtractType.PatientAdverseEvent;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(PatientAdverseEventExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public AdverseEventsMessageBag()
        {
        }

        public AdverseEventsMessageBag(IMessage<PatientAdverseEventView> message)
        {
            Message = message;
        }

        public AdverseEventsMessageBag(List<IMessage<PatientAdverseEventView>> messages)
        {
            Messages = messages;
        }

        public AdverseEventsMessageBag(AdverseEventsMessage message)
        {
            Message = message;
        }

        public static AdverseEventsMessageBag Create(PatientExtractView patient)
        {
            var message = new AdverseEventsMessage(patient);
            return new AdverseEventsMessageBag(message);
        }

        public IMessageBag<PatientAdverseEventView> Generate(List<PatientAdverseEventView> extracts)
        {
            var messages = new List<IMessage<PatientAdverseEventView>>();
            foreach (var adverseEventsExtractView in extracts)
            {
                var message = new AdverseEventsMessage(adverseEventsExtractView);
                messages.Add(message);
            }

            return new AdverseEventsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}


