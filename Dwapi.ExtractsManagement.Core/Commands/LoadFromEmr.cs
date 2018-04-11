using Dwapi.SharedKernel.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Commands
{
    public class LoadFromEmrCommand : IRequest<LoadFromEmrResponse>
    {
        public IList<DwhExtract> Extracts { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }

    }

    public class DwhExtract
    {
        public string Emr { get; set; }
        public string SqlQuery { get; set; }
        public ExtractType ExtractType { get; set; }
        public int Rank => (int)this.ExtractType;
    }

    public enum ExtractType
    {
        Patient,
        PatientArt,
        PatientBaseline,
        PatientLab,
        PatientPharmacy,
        PatientStatus,
        PatientVisit
    }

    public class LoadFromEmrResponse
    {

    }
}
