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
    public class IITRiskScoresMessageBag:IIITRiskScoresMessageBag
    {
        private int stake = 5;
        public string EndPoint => "IITRiskScores";
        public IMessage<IITRiskScoresExtractView> Message { get; set; }
        public List<IMessage<IITRiskScoresExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "IITRiskScoresExtract";
        public ExtractType ExtractType => ExtractType.IITRiskScores;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(IITRiskScoresExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public IITRiskScoresMessageBag()
        {
        }

        public IITRiskScoresMessageBag(IMessage<IITRiskScoresExtractView> message)
        {
            Message = message;
        }

        public IITRiskScoresMessageBag(List<IMessage<IITRiskScoresExtractView>> messages)
        {
            Messages = messages;
        }

        public IITRiskScoresMessageBag(IITRiskScoresMessage message)
        {
            Message = message;
        }

        public static IITRiskScoresMessageBag Create(PatientExtractView patient)
        {
            var message = new IITRiskScoresMessage(patient);
            return new IITRiskScoresMessageBag(message);
        }


        public IMessageBag<IITRiskScoresExtractView> Generate(List<IITRiskScoresExtractView> extracts)
        {
            var messages = new List<IMessage<IITRiskScoresExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new IITRiskScoresMessage(artExtractView);
                messages.Add(message);
            }

            return new IITRiskScoresMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
