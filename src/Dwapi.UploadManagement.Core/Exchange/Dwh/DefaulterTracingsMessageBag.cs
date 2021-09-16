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
    public class DefaulterTracingsMessageBag:IDefaulterTracingMessageBag
    {
        private int stake = 5;
        public string EndPoint => "DefaulterTracing";
        public IMessage<DefaulterTracingExtractView> Message { get; set; }
        public List<IMessage<DefaulterTracingExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "DefaulterTracingExtract";
        public ExtractType ExtractType => ExtractType.DefaulterTracing;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(DefaulterTracingExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public DefaulterTracingsMessageBag()
        {
        }

        public DefaulterTracingsMessageBag(IMessage<DefaulterTracingExtractView> message)
        {
            Message = message;
        }

        public DefaulterTracingsMessageBag(List<IMessage<DefaulterTracingExtractView>> messages)
        {
            Messages = messages;
        }

        public DefaulterTracingsMessageBag(DefaulterTracingsMessage message)
        {
            Message = message;
        }

        public static DefaulterTracingsMessageBag Create(PatientExtractView patient)
        {
            var message = new DefaulterTracingsMessage(patient);
            return new DefaulterTracingsMessageBag(message);
        }


        public IMessageBag<DefaulterTracingExtractView> Generate(List<DefaulterTracingExtractView> extracts)
        {
            var messages = new List<IMessage<DefaulterTracingExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new DefaulterTracingsMessage(artExtractView);
                messages.Add(message);
            }

            return new DefaulterTracingsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
