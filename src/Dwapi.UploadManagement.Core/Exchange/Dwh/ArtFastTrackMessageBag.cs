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
    public class ArtFastTrackMessageBag:IArtFastTrackMessageBag
    {
        private int stake = 5;
        public string EndPoint => "ArtFastTrack";
        public IMessage<ArtFastTrackExtractView> Message { get; set; }
        public List<IMessage<ArtFastTrackExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "ArtFastTrackExtract";
        public ExtractType ExtractType => ExtractType.ArtFastTrack;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(ArtFastTrackExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public ArtFastTrackMessageBag()
        {
        }

        public ArtFastTrackMessageBag(IMessage<ArtFastTrackExtractView> message)
        {
            Message = message;
        }

        public ArtFastTrackMessageBag(List<IMessage<ArtFastTrackExtractView>> messages)
        {
            Messages = messages;
        }

        public ArtFastTrackMessageBag(ArtFastTrackMessage message)
        {
            Message = message;
        }

        public static ArtFastTrackMessageBag Create(PatientExtractView patient)
        {
            var message = new ArtFastTrackMessage(patient);
            return new ArtFastTrackMessageBag(message);
        }


        public IMessageBag<ArtFastTrackExtractView> Generate(List<ArtFastTrackExtractView> extracts)
        {
            var messages = new List<IMessage<ArtFastTrackExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new ArtFastTrackMessage(artExtractView);
                messages.Add(message);
            }

            return new ArtFastTrackMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
