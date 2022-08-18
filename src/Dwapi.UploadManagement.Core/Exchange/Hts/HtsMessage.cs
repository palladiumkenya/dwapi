using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.UploadManagement.Core.Exchange.Hts
{
    public class HtsMessage
    {
        public List<HtsClients> Clients { get; set; }=new List<HtsClients>();
        public List<HtsClientTests> ClientTests { get; set; }=new List<HtsClientTests>();
        public List<HtsClientTracing> ClientTracing { get; set; }=new List<HtsClientTracing>();
        public List<HtsPartnerTracing> PartnerTracing { get; set; } = new List<HtsPartnerTracing>();
        public List<HtsPartnerNotificationServices> PartnerNotificationServices { get; set; } = new List<HtsPartnerNotificationServices>();
        public List<HtsTestKits> TestKits { get; set; } = new List<HtsTestKits>();
        public List<HtsClientLinkage> ClientLinkage { get; set; } = new List<HtsClientLinkage>();
        public List<HtsEligibilityExtract> HTSEligibility { get; set; } = new List<HtsEligibilityExtract>();

        public HtsMessage()
        {
        }

        public HtsMessage(List<HtsClients> clients)
        {
            Clients = clients;
        }
        public HtsMessage(List<HtsClientLinkage> clientsLinkage)
        {
            ClientLinkage = clientsLinkage;
        }
        public HtsMessage(List<HtsClientTests> clientTests)
        {
            ClientTests = clientTests;
        }
        public HtsMessage(List<HtsClientTracing> clientTracing)
        {
            ClientTracing = clientTracing;
        }
        public HtsMessage(List<HtsPartnerTracing> partnerTracing)
        {
            PartnerTracing = partnerTracing;
        }
        public HtsMessage(List<HtsPartnerNotificationServices> partnerNotificationServices)
        {
            PartnerNotificationServices = partnerNotificationServices;
        }
        public HtsMessage(List<HtsTestKits> testKits)
        {
            TestKits = testKits;
        }
        public HtsMessage(List<HtsEligibilityExtract> htsEligibilityExtract)
        {
            HTSEligibility = htsEligibilityExtract;
        }

        public static List<HtsMessage> Create(List<HtsClients> clients)
        {
            var list=new List<HtsMessage>();
            var chunks = clients.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }

        public static List<HtsMessage> Create(List<HtsClientTests> tests)
        {
            var list=new List<HtsMessage>();
            var chunks = tests.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }

        public static List<HtsMessage> Create(List<HtsClientTracing> cTracing)
        {
            var list=new List<HtsMessage>();
            var chunks = cTracing.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }

        public static List<HtsMessage> Create(List<HtsPartnerTracing> pTracing)
        {
            var list = new List<HtsMessage>();
            var chunks = pTracing.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }

        public static List<HtsMessage> Create(List<HtsPartnerNotificationServices> pns)
        {
            var list = new List<HtsMessage>();
            var chunks = pns.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }

        public static List<HtsMessage> Create(List<HtsClientLinkage> link)
        {
            var list = new List<HtsMessage>();
            var chunks = link.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }

        public static List<HtsMessage> Create(List<HtsTestKits> testkits)
        {
            var list = new List<HtsMessage>();
            var chunks = testkits.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }
        public static List<HtsMessage> Create(List<HtsEligibilityExtract> htsEligibilityExtract)
        {
            var list = new List<HtsMessage>();
            var chunks = htsEligibilityExtract.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new HtsMessage(chunk));
            }

            return list;
        }
    }
}
