using System.Collections.Generic;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Prep;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Prep
{
    public interface IPrepExtractReader
    {
        IEnumerable<Site> GetSites();
        IEnumerable<SitePatientProfile> GetSitePatientProfiles();
        IEnumerable<PatientPrepExtractView> ReadAllPatientPreps();
        IEnumerable<PrepAdverseEventExtractView> readAllAdverseEventExtracts();
        IEnumerable<PrepBehaviourRiskExtractView> readAllPrepBehaviourRisks();
        IEnumerable<PrepCareTerminationExtractView> readAllPrepCareTerminations();
        IEnumerable<PrepLabExtractView> ReadAllPrepLabs();
        IEnumerable<PrepPharmacyExtractView> readAllPrepPharmacys();
        IEnumerable<PrepVisitExtractView> ReadAllPrepVisits();
    }
}
