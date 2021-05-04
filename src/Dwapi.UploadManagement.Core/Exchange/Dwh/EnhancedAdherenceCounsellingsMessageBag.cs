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
    public class EnhancedAdherenceCounsellingsMessageBag:IEnhancedAdherenceCounsellingMessageBag
    {
        private int stake = 5;
        public string EndPoint => "EnhancedAdherenceCounselling";
        public IMessage<EnhancedAdherenceCounsellingExtractView> Message { get; set; }
        public List<IMessage<EnhancedAdherenceCounsellingExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "EnhancedAdherenceCounsellingExtract";
        public ExtractType ExtractType => ExtractType.EnhancedAdherenceCounselling;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(EnhancedAdherenceCounsellingExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public EnhancedAdherenceCounsellingsMessageBag()
        {
        }

        public EnhancedAdherenceCounsellingsMessageBag(IMessage<EnhancedAdherenceCounsellingExtractView> message)
        {
            Message = message;
        }

        public EnhancedAdherenceCounsellingsMessageBag(List<IMessage<EnhancedAdherenceCounsellingExtractView>> messages)
        {
            Messages = messages;
        }

        public EnhancedAdherenceCounsellingsMessageBag(EnhancedAdherenceCounsellingsMessage message)
        {
            Message = message;
        }

        public static EnhancedAdherenceCounsellingsMessageBag Create(PatientExtractView patient)
        {
            var message = new EnhancedAdherenceCounsellingsMessage(patient);
            return new EnhancedAdherenceCounsellingsMessageBag(message);
        }


        public IMessageBag<EnhancedAdherenceCounsellingExtractView> Generate(List<EnhancedAdherenceCounsellingExtractView> extracts)
        {
            var messages = new List<IMessage<EnhancedAdherenceCounsellingExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new EnhancedAdherenceCounsellingsMessage(artExtractView);
                messages.Add(message);
            }

            return new EnhancedAdherenceCounsellingsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
