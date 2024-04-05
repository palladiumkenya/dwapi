using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Mnch
{
    public interface IMnchPackager
    {
        IEnumerable<Manifest> Generate(EmrDto emrDto);
        IEnumerable<Manifest> GenerateWithMetrics(EmrDto emrDto);
        IEnumerable<PatientMnchExtract> GeneratePatientMnchs();
        IEnumerable<MnchEnrolmentExtract> GenerateMnchEnrolments();
        IEnumerable<MnchArtExtract> GenerateMnchArts();
        IEnumerable<AncVisitExtract> GenerateAncVisits();
        IEnumerable<MatVisitExtract> GenerateMatVisits();
        IEnumerable<PncVisitExtract> GeneratePncVisits();
        IEnumerable<MotherBabyPairExtract> GenerateMotherBabyPairs();
        IEnumerable<CwcEnrolmentExtract> GenerateCwcEnrolments();
        IEnumerable<CwcVisitExtract> GenerateCwcVisits();
        IEnumerable<HeiExtract> GenerateHeis();
        IEnumerable<MnchLabExtract> GenerateMnchLabs();
        IEnumerable<MnchImmunizationExtract> GenerateMnchImmunizations();

    }
}
