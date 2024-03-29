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
    public class PharmacyMessageBag:IPharmacyMessageBag
    {
        private int stake = 10;
        public string EndPoint => "PatientPharmacy";
        public IMessage<PatientPharmacyExtractView> Message { get; set; }
        public List<IMessage<PatientPharmacyExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "PatientPharmacyExtract";

        public ExtractType ExtractType => ExtractType.PatientPharmacy;

        public string Docket  => "NDWH";
        public string DocketExtract => nameof(PatientPharmacyExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public PharmacyMessageBag()
        {
        }

        public PharmacyMessageBag(IMessage<PatientPharmacyExtractView> message)
        {
            Message = message;
        }

        public PharmacyMessageBag(List<IMessage<PatientPharmacyExtractView>> messages)
        {
            Messages = messages;
        }

        public PharmacyMessageBag(PharmacyMessage message)
        {
            Message = message;
        }

        public static PharmacyMessageBag Create(PatientExtractView patient)
        {
            var message = new PharmacyMessage(patient);
            return new PharmacyMessageBag(message);
        }


        public IMessageBag<PatientPharmacyExtractView> Generate(List<PatientPharmacyExtractView> extracts)
        {
            var messages = new List<IMessage<PatientPharmacyExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new PharmacyMessage(artExtractView);
                messages.Add(message);
            }

            return new PharmacyMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
