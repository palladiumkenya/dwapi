using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class ArtMessage
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
        public List<PatientArtExtractView> ArtExtracts { get; set; }=new List<PatientArtExtractView>();

        public ArtMessage()
        {
        }

        public ArtMessage(PatientExtractView patient)
        {
            Demographic = patient;
            ArtExtracts = patient.PatientArtExtracts.ToList();
        }
        public bool HasContents => null != ArtExtracts && ArtExtracts.Any();
    }
}