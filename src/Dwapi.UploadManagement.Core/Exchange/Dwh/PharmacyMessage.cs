using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class PharmacyMessage
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
        public List<PatientPharmacyExtractView> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractView>();

        public PharmacyMessage()
        {
        }

        public PharmacyMessage(PatientExtractView patient)
        {
            Demographic = patient;
            PharmacyExtracts = patient.PatientPharmacyExtracts.ToList();
        }
        public bool HasContents => null != PharmacyExtracts && PharmacyExtracts.Any();

    }
}