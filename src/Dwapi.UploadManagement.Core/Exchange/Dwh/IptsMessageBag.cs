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
    public class IptsMessageBag:IIptMessageBag
    {
        private int stake = 30;
        public string EndPoint => "Ipts";
        public IMessage<IptExtractView> Message { get; set; }
        public List<IMessage<IptExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "IptExtract";
        public ExtractType ExtractType => ExtractType.Ipt;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(IptExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public IptsMessageBag()
        {
        }

        public IptsMessageBag(IMessage<IptExtractView> message)
        {
            Message = message;
        }

        public IptsMessageBag(List<IMessage<IptExtractView>> messages)
        {
            Messages = messages;
        }

        public IptsMessageBag(IptsMessage message)
        {
            Message = message;
        }

        public static IptsMessageBag Create(PatientExtractView patient)
        {
            var message = new IptsMessage(patient);
            return new IptsMessageBag(message);
        }


        public IMessageBag<IptExtractView> Generate(List<IptExtractView> extracts)
        {
            var messages = new List<IMessage<IptExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new IptsMessage(artExtractView);
                messages.Add(message);
            }

            return new IptsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
