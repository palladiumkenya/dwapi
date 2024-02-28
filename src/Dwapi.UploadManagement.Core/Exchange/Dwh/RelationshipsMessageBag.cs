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
    public class RelationshipsMessageBag:IRelationshipsMessageBag
    {
        private int stake = 5;
        public string EndPoint => "Relationships";
        public IMessage<RelationshipsExtractView> Message { get; set; }
        public List<IMessage<RelationshipsExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "RelationshipsExtract";
        public ExtractType ExtractType => ExtractType.Relationships;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(RelationshipsExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public RelationshipsMessageBag()
        {
        }

        public RelationshipsMessageBag(IMessage<RelationshipsExtractView> message)
        {
            Message = message;
        }

        public RelationshipsMessageBag(List<IMessage<RelationshipsExtractView>> messages)
        {
            Messages = messages;
        }

        public RelationshipsMessageBag(RelationshipsMessage message)
        {
            Message = message;
        }

        public static RelationshipsMessageBag Create(PatientExtractView patient)
        {
            var message = new RelationshipsMessage(patient);
            return new RelationshipsMessageBag(message);
        }


        public IMessageBag<RelationshipsExtractView> Generate(List<RelationshipsExtractView> extracts)
        {
            var messages = new List<IMessage<RelationshipsExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new RelationshipsMessage(artExtractView);
                messages.Add(message);
            }

            return new RelationshipsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
