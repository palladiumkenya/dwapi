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
    public class DrugAlcoholScreeningsMessageBag:IDrugAlcoholScreeningMessageBag
    {
        private int stake = 5;
        public string EndPoint => "DrugAlcoholScreening";
        public IMessage<DrugAlcoholScreeningExtractView> Message { get; set; }
        public List<IMessage<DrugAlcoholScreeningExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "DrugAlcoholScreeningExtract";
        public ExtractType ExtractType => ExtractType.DrugAlcoholScreening;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(DrugAlcoholScreeningExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public DrugAlcoholScreeningsMessageBag()
        {
        }

        public DrugAlcoholScreeningsMessageBag(IMessage<DrugAlcoholScreeningExtractView> message)
        {
            Message = message;
        }

        public DrugAlcoholScreeningsMessageBag(List<IMessage<DrugAlcoholScreeningExtractView>> messages)
        {
            Messages = messages;
        }

        public DrugAlcoholScreeningsMessageBag(DrugAlcoholScreeningsMessage message)
        {
            Message = message;
        }

        public static DrugAlcoholScreeningsMessageBag Create(PatientExtractView patient)
        {
            var message = new DrugAlcoholScreeningsMessage(patient);
            return new DrugAlcoholScreeningsMessageBag(message);
        }


        public IMessageBag<DrugAlcoholScreeningExtractView> Generate(List<DrugAlcoholScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<DrugAlcoholScreeningExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new DrugAlcoholScreeningsMessage(artExtractView);
                messages.Add(message);
            }

            return new DrugAlcoholScreeningsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
