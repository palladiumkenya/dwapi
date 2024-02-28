﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class RelationshipsMessage:IRelationshipsMessage
    {
        public Facility Facility
        {
            get
            {
                var facility = Demographic?.GetFacility();
                if (null != facility)
                    return facility;
                return new Facility();
            }
        }
        public PatientExtractView Demographic { get; set; }
        [JsonProperty(PropertyName = "RelationshipsExtracts")]
        public List<RelationshipsExtractView> Extracts { get; } = new List<RelationshipsExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "RelationshipsExtracts")]
        public List<RelationshipsExtractView> RelationshipsExtracts { get; set; } = new List<RelationshipsExtractView>();
        public bool HasContents => null != RelationshipsExtracts && RelationshipsExtracts.Any();

        public RelationshipsMessage()
        {
        }

        public RelationshipsMessage(RelationshipsExtractView patient)
        {
            patient.PatientExtractView.RelationshipsExtracts=new List<RelationshipsExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            RelationshipsExtracts.Add(patient);
        }

        public RelationshipsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.RelationshipsExtracts.ToList();
            RelationshipsExtracts = patient.RelationshipsExtracts.ToList();
        }

        public IMessage<RelationshipsExtractView> Generate(RelationshipsExtractView extract)
        {
            return new RelationshipsMessage(extract);
        }

        public List<IMessage<RelationshipsExtractView>> GenerateMessages(List<RelationshipsExtractView> extracts)
        {
            var messages = new List<IMessage<RelationshipsExtractView>>();
            extracts.ForEach(e => messages.Add(Generate(e)));
            return messages;
        }

        private List<Guid> GetIds()
        {
            if (Extracts.Any())
                return Extracts.Select(x => x.Id).ToList();

            return new List<Guid>();
        }
    }
}
