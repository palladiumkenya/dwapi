using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Prep;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Prep;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Packager.Prep
{
    public class PrepPackager : IPrepPackager
    {
        private readonly IPrepExtractReader _prepExtractReader;
        private readonly IEmrMetricReader _metricReader;

        public PrepPackager(IPrepExtractReader prepExtractReader, IEmrMetricReader metricReader)
        {
            _prepExtractReader = prepExtractReader;
            _metricReader = metricReader;
        }


        public IEnumerable<Manifest> Generate(EmrDto emrSetup)
        {
            var sites = _prepExtractReader.GetSites();
            var profiles = _prepExtractReader.GetSitePatientProfiles();

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

        public IEnumerable<PatientPrepExtract> GeneratePatientPreps()
        {
            return _prepExtractReader.ReadAllPatientPreps();
        }
        public IEnumerable<PrepAdverseEventExtract> GeneratePrepAdverseEvents()
        {
            return _prepExtractReader.ReadAllPrepAdverseEvents();
        }
        public IEnumerable<PrepBehaviourRiskExtract> GeneratePrepBehaviourRisks()
        {
            return _prepExtractReader.ReadAllPrepBehaviourRisks();
        }
        public IEnumerable<PrepCareTerminationExtract> GeneratePrepCareTerminations()
        {
            return _prepExtractReader.ReadAllPrepCareTerminations();
        }
        public IEnumerable<PrepLabExtract> GeneratePrepLabs()
        {
            return _prepExtractReader.ReadAllPrepLabs();
        }
        public IEnumerable<PrepPharmacyExtract> GeneratePrepPharmacys()
        {
            return _prepExtractReader.ReadAllPrepPharmacys();
        }
        public IEnumerable<PrepVisitExtract> GeneratePrepVisits()
        {
            return _prepExtractReader.ReadAllPrepVisits();
        }
        public IEnumerable<PrepMonthlyRefillExtract> GeneratePrepMonthlyRefill()
        {
            return _prepExtractReader.ReadAllPrepMonthlyRefill();
        }
    }
}
