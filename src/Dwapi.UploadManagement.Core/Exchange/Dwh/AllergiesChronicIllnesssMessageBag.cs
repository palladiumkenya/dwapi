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
    public class AllergiesChronicIllnesssMessageBag:IAllergiesChronicIllnessMessageBag
    {
        private int stake = 5;
        public string EndPoint => "AllergiesChronicIllness";
        public IMessage<AllergiesChronicIllnessExtractView> Message { get; set; }
        public List<IMessage<AllergiesChronicIllnessExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "AllergiesChronicIllnessExtract";
        public ExtractType ExtractType => ExtractType.AllergiesChronicIllness;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(AllergiesChronicIllnessExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public AllergiesChronicIllnesssMessageBag()
        {
        }

        public AllergiesChronicIllnesssMessageBag(IMessage<AllergiesChronicIllnessExtractView> message)
        {
            Message = message;
        }

        public AllergiesChronicIllnesssMessageBag(List<IMessage<AllergiesChronicIllnessExtractView>> messages)
        {
            Messages = messages;
        }

        public AllergiesChronicIllnesssMessageBag(AllergiesChronicIllnesssMessage message)
        {
            Message = message;
        }

        public static AllergiesChronicIllnesssMessageBag Create(PatientExtractView patient)
        {
            var message = new AllergiesChronicIllnesssMessage(patient);
            return new AllergiesChronicIllnesssMessageBag(message);
        }


        public IMessageBag<AllergiesChronicIllnessExtractView> Generate(List<AllergiesChronicIllnessExtractView> extracts)
        {
            var messages = new List<IMessage<AllergiesChronicIllnessExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new AllergiesChronicIllnesssMessage(artExtractView);
                messages.Add(message);
            }

            return new AllergiesChronicIllnesssMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
