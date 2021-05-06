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
    public class OtzsMessageBag:IOtzMessageBag
    {
        private int stake = 5;
        public string EndPoint => "Otz";
        public IMessage<OtzExtractView> Message { get; set; }
        public List<IMessage<OtzExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "OtzExtract";
        public ExtractType ExtractType => ExtractType.Otz;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(OtzExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public OtzsMessageBag()
        {
        }

        public OtzsMessageBag(IMessage<OtzExtractView> message)
        {
            Message = message;
        }

        public OtzsMessageBag(List<IMessage<OtzExtractView>> messages)
        {
            Messages = messages;
        }

        public OtzsMessageBag(OtzsMessage message)
        {
            Message = message;
        }

        public static OtzsMessageBag Create(PatientExtractView patient)
        {
            var message = new OtzsMessage(patient);
            return new OtzsMessageBag(message);
        }


        public IMessageBag<OtzExtractView> Generate(List<OtzExtractView> extracts)
        {
            var messages = new List<IMessage<OtzExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new OtzsMessage(artExtractView);
                messages.Add(message);
            }

            return new OtzsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
