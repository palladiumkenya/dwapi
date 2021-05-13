using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.UploadManagement.Core.Exchange.Mnch
{
    public class MnchMessage
    {
        public List<PatientMnchExtract> PatientMnchExtracts { get; set; } = new List<PatientMnchExtract>();
        public List<MnchEnrolmentExtract> MnchEnrolmentExtracts { get; set; } = new List<MnchEnrolmentExtract>();
        public List<MnchArtExtract> MnchArtExtracts { get; set; } = new List<MnchArtExtract>();
        public List<AncVisitExtract> AncVisitExtracts { get; set; } = new List<AncVisitExtract>();
        public List<MatVisitExtract> MatVisitExtracts { get; set; } = new List<MatVisitExtract>();
        public List<PncVisitExtract> PncVisitExtracts { get; set; } = new List<PncVisitExtract>();
        public List<MotherBabyPairExtract> MotherBabyPairExtracts { get; set; } = new List<MotherBabyPairExtract>();
        public List<CwcEnrolmentExtract> CwcEnrolmentExtracts { get; set; } = new List<CwcEnrolmentExtract>();
        public List<CwcVisitExtract> CwcVisitExtracts { get; set; } = new List<CwcVisitExtract>();
        public List<HeiExtract> HeiExtracts { get; set; } = new List<HeiExtract>();
        public List<MnchLabExtract> MnchLabExtracts { get; set; } = new List<MnchLabExtract>();

        public MnchMessage()
        {
        }

        public MnchMessage(List<PatientMnchExtract> extracts)
        {
            PatientMnchExtracts = extracts;
        }

        public MnchMessage(List<MnchEnrolmentExtract> extracts)
        {
            MnchEnrolmentExtracts = extracts;
        }

        public MnchMessage(List<MnchArtExtract> extracts)
        {
            MnchArtExtracts = extracts;
        }

        public MnchMessage(List<AncVisitExtract> extracts)
        {
            AncVisitExtracts = extracts;
        }

        public MnchMessage(List<MatVisitExtract> extracts)
        {
            MatVisitExtracts = extracts;
        }

        public MnchMessage(List<PncVisitExtract> extracts)
        {
            PncVisitExtracts = extracts;
        }

        public MnchMessage(List<MotherBabyPairExtract> extracts)
        {
            MotherBabyPairExtracts = extracts;
        }

        public MnchMessage(List<CwcEnrolmentExtract> extracts)
        {
            CwcEnrolmentExtracts = extracts;
        }

        public MnchMessage(List<CwcVisitExtract> extracts)
        {
            CwcVisitExtracts = extracts;
        }

        public MnchMessage(List<HeiExtract> extracts)
        {
            HeiExtracts = extracts;
        }

        public MnchMessage(List<MnchLabExtract> extracts)
        {
            MnchLabExtracts = extracts;
        }


        public static List<MnchMessage> Create(List<PatientMnchExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<MnchEnrolmentExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<MnchArtExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<AncVisitExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<MatVisitExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<PncVisitExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<MotherBabyPairExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<CwcEnrolmentExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<CwcVisitExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<HeiExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

        public static List<MnchMessage> Create(List<MnchLabExtract> extracts)
        {
            var list = new List<MnchMessage>();
            var chunks = extracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MnchMessage(chunk));
            }

            return list;
        }

    }
}
