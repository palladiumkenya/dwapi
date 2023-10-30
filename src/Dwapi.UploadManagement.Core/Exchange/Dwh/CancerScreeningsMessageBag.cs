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
    public class CancerScreeningsMessageBag:ICancerScreeningMessageBag
    {
        private int stake = 5;
        public string EndPoint => "CancerScreening";
        public IMessage<CancerScreeningExtractView> Message { get; set; }
        public List<IMessage<CancerScreeningExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "CancerScreeningExtract";
        public ExtractType ExtractType => ExtractType.CancerScreening;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(CancerScreeningExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public CancerScreeningsMessageBag()
        {
        }

        public CancerScreeningsMessageBag(IMessage<CancerScreeningExtractView> message)
        {
            Message = message;
        }

        public CancerScreeningsMessageBag(List<IMessage<CancerScreeningExtractView>> messages)
        {
            Messages = messages;
        }

        public CancerScreeningsMessageBag(CancerScreeningsMessage message)
        {
            Message = message;
        }

        public static CancerScreeningsMessageBag Create(PatientExtractView patient)
        {
            var message = new CancerScreeningsMessage(patient);
            return new CancerScreeningsMessageBag(message);
        }


        public IMessageBag<CancerScreeningExtractView> Generate(List<CancerScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<CancerScreeningExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new CancerScreeningsMessage(artExtractView);
                messages.Add(message);
            }

            return new CancerScreeningsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
