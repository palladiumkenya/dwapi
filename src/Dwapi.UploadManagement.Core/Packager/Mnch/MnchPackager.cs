using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mnch;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mnch;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Packager.Mnch
{
    public class MnchPackager : IMnchPackager
    {
        private readonly IMnchExtractReader _mnchExtractReader;
        private readonly IEmrMetricReader _metricReader;

        public MnchPackager(IMnchExtractReader mnchExtractReader, IEmrMetricReader metricReader)
        {
            _mnchExtractReader = mnchExtractReader;
            _metricReader = metricReader;
        }


        public IEnumerable<Manifest> Generate(EmrDto emrSetup)
        {
            var sites = _mnchExtractReader.GetSites();
            var profiles = _mnchExtractReader.GetSitePatientProfiles();

            return Manifest.Create(profiles, emrSetup, sites);
        }

        public IEnumerable<Manifest> GenerateWithMetrics(EmrDto emrSetup)
        {
            var metrics = _metricReader.ReadAll().FirstOrDefault();
            var appMetrics = _metricReader.ReadAppAll().ToList();
            var manifests = Generate(emrSetup).ToList();

            if (null != metrics)
            {
                foreach (var manifest in manifests)
                {
                    manifest.AddCargo(metrics);
                    manifest.AddAppToCargo<AppMetricView>(appMetrics);
                }
            }

            return manifests;
        }

        public IEnumerable<PatientMnchExtract> GeneratePatientMnchs()
        {
            return _mnchExtractReader.ReadAllPatientMnchs();
        }

        public IEnumerable<MnchEnrolmentExtract> GenerateMnchEnrolments()
        {
            return _mnchExtractReader.ReadAllMnchEnrolments();
        }

        public IEnumerable<MnchArtExtract> GenerateMnchArts()
        {
            return _mnchExtractReader.ReadAllMnchArts();
        }

        public IEnumerable<AncVisitExtract> GenerateAncVisits()
        {
            return _mnchExtractReader.ReadAllAncVisits();
        }

        public IEnumerable<MatVisitExtract> GenerateMatVisits()
        {
            return _mnchExtractReader.ReadAllMatVisits();
        }

        public IEnumerable<PncVisitExtract> GeneratePncVisits()
        {
            return _mnchExtractReader.ReadAllPncVisits();
        }

        public IEnumerable<MotherBabyPairExtract> GenerateMotherBabyPairs()
        {
            return _mnchExtractReader.ReadAllMotherBabyPairs();
        }

        public IEnumerable<CwcEnrolmentExtract> GenerateCwcEnrolments()
        {
            return _mnchExtractReader.ReadAllCwcEnrolments();
        }

        public IEnumerable<CwcVisitExtract> GenerateCwcVisits()
        {
            return _mnchExtractReader.ReadAllCwcVisits();
        }

        public IEnumerable<HeiExtract> GenerateHeis()
        {
            return _mnchExtractReader.ReadAllHeis();
        }

        public IEnumerable<MnchLabExtract> GenerateMnchLabs()
        {
            return _mnchExtractReader.ReadAllMnchLabs();
        }

    }
}
