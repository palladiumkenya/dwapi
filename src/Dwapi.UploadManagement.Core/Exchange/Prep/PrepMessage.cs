using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.SharedKernel.Utility;
using Humanizer;

namespace Dwapi.UploadManagement.Core.Exchange.Prep
{
    public class PrepMessage
    {
        private static readonly int batch = 200;
        public List<PatientPrepExtract> PatientPrepExtracts { get; set; } = new List<PatientPrepExtract>();
        public List<PrepAdverseEventExtract> PrepAdverseEventExtracts { get; set; } = new List<PrepAdverseEventExtract>();
        public List<PrepBehaviourRiskExtract> PrepBehaviourRiskExtracts { get; set; } = new List<PrepBehaviourRiskExtract>();
        public List<PrepCareTerminationExtract> PrepCareTerminationExtracts { get; set; } = new List<PrepCareTerminationExtract>();
        public List<PrepLabExtract> PrepLabExtracts { get; set; } = new List<PrepLabExtract>();
        public List<PrepPharmacyExtract> PrepPharmacyExtracts { get; set; } = new List<PrepPharmacyExtract>();
        public List<PrepVisitExtract> PrepVisitExtracts { get; set; } = new List<PrepVisitExtract>();
        public List<PrepMonthlyRefillExtract> PrepMonthlyRefillExtracts { get; set; } = new List<PrepMonthlyRefillExtract>();

        public PrepMessage()
        {
        }

        public PrepMessage(List<PatientPrepExtract> extracts)
        {
            PatientPrepExtracts = extracts;
        }
        public PrepMessage(List<PrepAdverseEventExtract> extracts)
        {
            PrepAdverseEventExtracts = extracts;
        }
        public PrepMessage(List<PrepBehaviourRiskExtract> extracts)
        {
            PrepBehaviourRiskExtracts = extracts;
        }
        public PrepMessage(List<PrepCareTerminationExtract> extracts)
        {
            PrepCareTerminationExtracts = extracts;
        }

        public PrepMessage(List<PrepLabExtract> extracts)
        {
            PrepLabExtracts = extracts;
        }

        public PrepMessage(List<PrepPharmacyExtract> extracts)
        {
            PrepPharmacyExtracts = extracts;
        }
        public PrepMessage(List<PrepVisitExtract> extracts)
        {
            PrepVisitExtracts = extracts;
        }
        
        public PrepMessage(List<PrepMonthlyRefillExtract> extracts)
        {
            PrepMonthlyRefillExtracts = extracts;
        }

        public static List<PrepMessage> Create(List<PatientPrepExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList().ChunkBy(batch);
            foreach (var chunk in chunks)
            {
                list.Add(new PrepMessage(chunk));
            }

            return list;
        }

        public static List<PrepMessage> Create(List<PrepAdverseEventExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList().ChunkBy(batch);
            foreach (var chunk in chunks)
            {
                list.Add(new PrepMessage(chunk));
            }

            return list;
        }
        public static List<PrepMessage> Create(List<PrepBehaviourRiskExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList().ChunkBy(batch);
            foreach (var chunk in chunks)
            {
                list.Add(new PrepMessage(chunk));
            }

            return list;
        }


        public static List<PrepMessage> Create(List<PrepCareTerminationExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList().ChunkBy(batch);
            foreach (var chunk in chunks)
            {
                list.Add(new PrepMessage(chunk));
            }

            return list;
        }

        public static List<PrepMessage> Create(List<PrepLabExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList().ChunkBy(batch);
            foreach (var chunk in chunks)
            {
                list.Add(new PrepMessage(chunk));
            }

            return list;
        }
        public static List<PrepMessage> Create(List<PrepPharmacyExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList().ChunkBy(batch);
            foreach (var chunk in chunks)
            {
                list.Add(new PrepMessage(chunk));
            }

            return list;
        }

        public static List<PrepMessage> Create(List<PrepVisitExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList().ChunkBy(batch);
            foreach (var chunk in chunks)
            {
                list.Add(new PrepMessage(chunk));
            }

            return list;
        }
        
        public static List<PrepMessage> Create(List<PrepMonthlyRefillExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList().ChunkBy(batch);
            foreach (var chunk in chunks)
            {
                list.Add(new PrepMessage(chunk));
            }

            return list;
        }

        //BoardRoom Uploads
        public static List<PrepMessage> CreateEx(List<PatientPrepExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList();
            list.Add(new PrepMessage(chunks));

            return list;
        }
        public static List<PrepMessage> CreateEx(List<PrepAdverseEventExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList();
            list.Add(new PrepMessage(chunks));

            return list;

        }
        public static List<PrepMessage> CreateEx(List<PrepBehaviourRiskExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList();
            list.Add(new PrepMessage(chunks));

            return list;
        }
        public static List<PrepMessage> CreateEx(List<PrepCareTerminationExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList();
            list.Add(new PrepMessage(chunks));

            return list;
        }
        public static List<PrepMessage> CreateEx(List<PrepLabExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList();
            list.Add(new PrepMessage(chunks));

            return list;
        }
        public static List<PrepMessage> CreateEx(List<PrepPharmacyExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList();
            list.Add(new PrepMessage(chunks));

            return list;
        }
        public static List<PrepMessage> CreateEx(List<PrepVisitExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList();
            list.Add(new PrepMessage(chunks));

            return list;
        }
        
        public static List<PrepMessage> CreateEx(List<PrepMonthlyRefillExtract> extracts)
        {
            var list = new List<PrepMessage>();
            var chunks = extracts.ToList();
            list.Add(new PrepMessage(chunks));

            return list;
        }

    }
}
