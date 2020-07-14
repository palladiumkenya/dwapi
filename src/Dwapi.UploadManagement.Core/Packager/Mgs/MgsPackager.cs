using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mgs;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Packager.Mgs
{
    public class MgsPackager : IMgsPackager
    {
        private readonly IMgsExtractReader _htsExtractReader;
        private readonly IEmrMetricReader _metricReader;

        public MgsPackager(IMgsExtractReader htsExtractReader, IEmrMetricReader metricReader)
        {
            _htsExtractReader = htsExtractReader;
            _metricReader = metricReader;
        }

        public IEnumerable<Manifest>  Generate(EmrDto emrSetup)
        {
            var sites = _htsExtractReader.GetSites();
            var profiles = _htsExtractReader.GetSitePatientProfiles();

            return Manifest.Create(profiles, emrSetup,sites);
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

        public IEnumerable<MetricMigrationExtract> GenerateMigrations()
        {
            return _htsExtractReader.ReadAllMigrations();
        }
    }
}
