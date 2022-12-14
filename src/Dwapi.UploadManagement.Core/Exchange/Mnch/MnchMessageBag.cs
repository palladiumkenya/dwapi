using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;

namespace Dwapi.UploadManagement.Core.Exchange.Mnch
{
    public class MnchMessageBag
    {
        public List<MnchMessage> Messages { get; set; } = new List<MnchMessage>();

        public MnchMessageBag()
        {
        }
        public MnchMessageBag(List<MnchMessage> messages)
        {
            Messages = messages;
        }

        public static MnchMessageBag Create(List<PatientMnchExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<MnchEnrolmentExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<MnchArtExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<AncVisitExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<MatVisitExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<PncVisitExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<MotherBabyPairExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<CwcEnrolmentExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<CwcVisitExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<HeiExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        public static MnchMessageBag Create(List<MnchLabExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.Create(extracts));
        }

        //BoardRoomUploads
        public static MnchMessageBag CreateEx(List<PatientMnchExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<MnchEnrolmentExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<MnchArtExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<AncVisitExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<MatVisitExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<PncVisitExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<MotherBabyPairExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<CwcEnrolmentExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<CwcVisitExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
        public static MnchMessageBag CreateEx(List<HeiExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }

        public static MnchMessageBag CreateEx(List<MnchLabExtract> extracts)
        {
            return new MnchMessageBag(MnchMessage.CreateEx(extracts));
        }
    }
}
