﻿using System;
using System.Collections.Generic;
using System.Linq;
 using Dwapi.SharedKernel.Enum;
 using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class StatusMessageBag:IStatusMessageBag
    {
        public string EndPoint => "PatientStatus";
        public IMessage<PatientStatusExtractView> Message { get; set; }
        public List<IMessage<PatientStatusExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "PatientStatusExtract";
        public ExtractType ExtractType => ExtractType.PatientStatus;
        public StatusMessageBag()
        {
        }

        public StatusMessageBag(IMessage<PatientStatusExtractView> message)
        {
            Message = message;
        }

        public StatusMessageBag(List<IMessage<PatientStatusExtractView>> messages)
        {
            Messages = messages;
        }

        public StatusMessageBag(StatusMessage message)
        {
            Message = message;
        }

        public static StatusMessageBag Create(PatientExtractView patient)
        {
            var message = new StatusMessage(patient);
            return new StatusMessageBag(message);
        }


        public IMessageBag<PatientStatusExtractView> Generate(List<PatientStatusExtractView> extracts)
        {
            var messages = new List<IMessage<PatientStatusExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new StatusMessage(artExtractView);
                messages.Add(message);
            }

            return new StatusMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
