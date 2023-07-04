using System;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mnch
{
    public class TempMotherBabyPairExtract : TempExtract, IMotherBabyPair
    {
        public string FacilityName { get; set; }
        public int BabyPatientPK { get; set; }
        public int MotherPatientPK { get; set; }
        public string BabyPatientMncHeiID { get; set; }
        public string MotherPatientMncHeiID { get; set; }
        public string PatientIDCCC { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
