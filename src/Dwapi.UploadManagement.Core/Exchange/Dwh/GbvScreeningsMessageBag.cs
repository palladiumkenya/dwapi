﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
 using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
 using Dwapi.SharedKernel.Enum;
 using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class GbvScreeningsMessageBag:IGbvScreeningMessageBag
    {
        private int stake = 5;
        public string EndPoint => "GbvScreening";
        public IMessage<GbvScreeningExtractView> Message { get; set; }
        public List<IMessage<GbvScreeningExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "GbvScreeningExtract";
        public ExtractType ExtractType => ExtractType.GbvScreening;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(GbvScreeningExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public GbvScreeningsMessageBag()
        {
        }

        public GbvScreeningsMessageBag(IMessage<GbvScreeningExtractView> message)
        {
            Message = message;
        }

        public GbvScreeningsMessageBag(List<IMessage<GbvScreeningExtractView>> messages)
        {
            Messages = messages;
        }

        public GbvScreeningsMessageBag(GbvScreeningsMessage message)
        {
            Message = message;
        }

        public static GbvScreeningsMessageBag Create(PatientExtractView patient)
        {
            var message = new GbvScreeningsMessage(patient);
            return new GbvScreeningsMessageBag(message);
        }


        public IMessageBag<GbvScreeningExtractView> Generate(List<GbvScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<GbvScreeningExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new GbvScreeningsMessage(artExtractView);
                messages.Add(message);
            }

            return new GbvScreeningsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
