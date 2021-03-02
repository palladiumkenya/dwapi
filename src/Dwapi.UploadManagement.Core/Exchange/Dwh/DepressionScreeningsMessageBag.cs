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
    public class DepressionScreeningsMessageBag:IDepressionScreeningMessageBag
    {
        private int stake = 30;
        public string EndPoint => "DepressionScreenings";
        public IMessage<DepressionScreeningExtractView> Message { get; set; }
        public List<IMessage<DepressionScreeningExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "DepressionScreeningExtract";
        public ExtractType ExtractType => ExtractType.DepressionScreening;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(DepressionScreeningExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public DepressionScreeningsMessageBag()
        {
        }

        public DepressionScreeningsMessageBag(IMessage<DepressionScreeningExtractView> message)
        {
            Message = message;
        }

        public DepressionScreeningsMessageBag(List<IMessage<DepressionScreeningExtractView>> messages)
        {
            Messages = messages;
        }

        public DepressionScreeningsMessageBag(DepressionScreeningsMessage message)
        {
            Message = message;
        }

        public static DepressionScreeningsMessageBag Create(PatientExtractView patient)
        {
            var message = new DepressionScreeningsMessage(patient);
            return new DepressionScreeningsMessageBag(message);
        }


        public IMessageBag<DepressionScreeningExtractView> Generate(List<DepressionScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<DepressionScreeningExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new DepressionScreeningsMessage(artExtractView);
                messages.Add(message);
            }

            return new DepressionScreeningsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
