using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.UploadManagement.Core.Exchange.Hts
{
    public class HtsMessage
    {
        public List<HTSClientExtract> Clients { get; set; }=new List<HTSClientExtract>();
        public List<HTSClientLinkageExtract> ClientLinkages { get; set; }=new List<HTSClientLinkageExtract>();
        public List<HTSClientPartnerExtract> ClientPartners { get; set; }=new List<HTSClientPartnerExtract>();

        public HtsMessage()
        {
        }

        public HtsMessage(List<HTSClientExtract> clients)
        {
            Clients = clients;
        }
        public HtsMessage(List<HTSClientLinkageExtract> links)
        {
            ClientLinkages = links;
        }
        public HtsMessage(List<HTSClientPartnerExtract> partnerExtracts)
        {
            ClientPartners = partnerExtracts;
        }

        public static List<HtsMessage> Create(List<HTSClientExtract> masterPatientIndices)
        {
            var list=new List<HtsMessage>();
            var chunks = masterPatientIndices.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }

        public static List<HtsMessage> Create(List<HTSClientLinkageExtract> links)
        {
            var list=new List<HtsMessage>();
            var chunks = links.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }

        public static List<HtsMessage> Create(List<HTSClientPartnerExtract> partnerExtracts)
        {
            var list=new List<HtsMessage>();
            var chunks = partnerExtracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }
    }
}
