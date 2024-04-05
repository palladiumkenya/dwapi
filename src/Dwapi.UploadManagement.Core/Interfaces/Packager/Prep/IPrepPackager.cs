using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Prep
{
    public interface IPrepPackager
    {
        IEnumerable<Manifest> Generate(EmrDto emrDto);
        IEnumerable<Manifest> GenerateWithMetrics(EmrDto emrDto);
        IEnumerable<PatientPrepExtract> GeneratePatientPreps();
        IEnumerable<PrepAdverseEventExtract> GeneratePrepAdverseEvents();
        IEnumerable<PrepBehaviourRiskExtract> GeneratePrepBehaviourRisks();
        IEnumerable<PrepCareTerminationExtract> GeneratePrepCareTerminations();
        IEnumerable<PrepLabExtract> GeneratePrepLabs();
        IEnumerable<PrepPharmacyExtract> GeneratePrepPharmacys();
        IEnumerable<PrepVisitExtract> GeneratePrepVisits();
        IEnumerable<PrepMonthlyRefillExtract> GeneratePrepMonthlyRefill();

    }
}
