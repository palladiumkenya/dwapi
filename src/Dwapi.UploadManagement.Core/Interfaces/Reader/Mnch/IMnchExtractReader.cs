using System.Collections.Generic;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Mnch;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Mnch
{
    public interface IMnchExtractReader
    {
        IEnumerable<Site> GetSites();
        IEnumerable<SitePatientProfile> GetSitePatientProfiles();
        IEnumerable<PatientMnchExtractView> ReadAllPatientMnchs();
        IEnumerable<MnchEnrolmentExtractView> ReadAllMnchEnrolments();
        IEnumerable<MnchArtExtractView> ReadAllMnchArts();
        IEnumerable<AncVisitExtractView> ReadAllAncVisits();
        IEnumerable<MatVisitExtractView> ReadAllMatVisits();
        IEnumerable<PncVisitExtractView> ReadAllPncVisits();
        IEnumerable<MotherBabyPairExtractView> ReadAllMotherBabyPairs();
        IEnumerable<CwcEnrolmentExtractView> ReadAllCwcEnrolments();
        IEnumerable<CwcVisitExtractView> ReadAllCwcVisits();
        IEnumerable<HeiExtractView> ReadAllHeis();
        IEnumerable<MnchLabExtractView> ReadAllMnchLabs();
        IEnumerable<MnchImmunizationExtractView> ReadAllMnchImmunizations();

    }
}
