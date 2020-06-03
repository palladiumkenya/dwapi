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
    public class ArtMessageBag : IArtMessageBag
    {
        public string EndPoint => "PatientArt";
        public IMessage<PatientArtExtractView> Message { get; set; }
        public List<IMessage<PatientArtExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "PatientArtExtract";
        public ExtractType ExtractType => ExtractType.PatientArt;
        public ArtMessageBag()
        {
        }

        public ArtMessageBag(IMessage<PatientArtExtractView> message)
        {
            Message = message;
        }

        public ArtMessageBag(List<IMessage<PatientArtExtractView>> messages)
        {
            Messages = messages;
        }

        public ArtMessageBag(ArtMessage message)
        {
            Message = message;
        }

        public static ArtMessageBag Create(PatientExtractView patient)
        {
            var message = new ArtMessage(patient);
            return new ArtMessageBag(message);
        }


        public IMessageBag<PatientArtExtractView> Generate(List<PatientArtExtractView> extracts)
        {
            var messages = new List<IMessage<PatientArtExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new ArtMessage(artExtractView);
                messages.Add(message);
            }

            return new ArtMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }


    }
}
