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
    public class CervicalCancerScreeningsMessageBag:ICervicalCancerScreeningMessageBag
    {
        private int stake = 5;
        public string EndPoint => "CervicalCancerScreening";
        public IMessage<CervicalCancerScreeningExtractView> Message { get; set; }
        public List<IMessage<CervicalCancerScreeningExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "CervicalCancerScreeningExtract";
        public ExtractType ExtractType => ExtractType.CervicalCancerScreening;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(CervicalCancerScreeningExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public CervicalCancerScreeningsMessageBag()
        {
        }

        public CervicalCancerScreeningsMessageBag(IMessage<CervicalCancerScreeningExtractView> message)
        {
            Message = message;
        }

        public CervicalCancerScreeningsMessageBag(List<IMessage<CervicalCancerScreeningExtractView>> messages)
        {
            Messages = messages;
        }

        public CervicalCancerScreeningsMessageBag(CervicalCancerScreeningsMessage message)
        {
            Message = message;
        }

        public static CervicalCancerScreeningsMessageBag Create(PatientExtractView patient)
        {
            var message = new CervicalCancerScreeningsMessage(patient);
            return new CervicalCancerScreeningsMessageBag(message);
        }


        public IMessageBag<CervicalCancerScreeningExtractView> Generate(List<CervicalCancerScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<CervicalCancerScreeningExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new CervicalCancerScreeningsMessage(artExtractView);
                messages.Add(message);
            }

            return new CervicalCancerScreeningsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
