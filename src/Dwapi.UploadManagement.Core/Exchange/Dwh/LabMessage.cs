using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class LabMessage
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
        public List<PatientLaboratoryExtractView> LaboratoryExtracts { get; set; } = new List<PatientLaboratoryExtractView>();

        public LabMessage()
        {
        }

        public LabMessage(PatientExtractView patient)
        {
            Demographic = patient;
            LaboratoryExtracts = patient.PatientLaboratoryExtracts.ToList();
        }
        public bool HasContents => null != LaboratoryExtracts && LaboratoryExtracts.Any();
    }
}