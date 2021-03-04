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
    public class OvcsMessageBag:IOvcMessageBag
    {
        private int stake = 5;
        public string EndPoint => "Ovc";
        public IMessage<OvcExtractView> Message { get; set; }
        public List<IMessage<OvcExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "OvcExtract";
        public ExtractType ExtractType => ExtractType.Ovc;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(OvcExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public OvcsMessageBag()
        {
        }

        public OvcsMessageBag(IMessage<OvcExtractView> message)
        {
            Message = message;
        }

        public OvcsMessageBag(List<IMessage<OvcExtractView>> messages)
        {
            Messages = messages;
        }

        public OvcsMessageBag(OvcsMessage message)
        {
            Message = message;
        }

        public static OvcsMessageBag Create(PatientExtractView patient)
        {
            var message = new OvcsMessage(patient);
            return new OvcsMessageBag(message);
        }


        public IMessageBag<OvcExtractView> Generate(List<OvcExtractView> extracts)
        {
            var messages = new List<IMessage<OvcExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new OvcsMessage(artExtractView);
                messages.Add(message);
            }

            return new OvcsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
