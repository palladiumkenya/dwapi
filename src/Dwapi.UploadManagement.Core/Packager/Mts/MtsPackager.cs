using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Mts;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Packager.Mts
{
    public class MtsPackager : IMtsPackager
    {
        private readonly IMtsExtractReader _htsExtractReader;
        private readonly IEmrMetricReader _metricReader;

        public MtsPackager(IMtsExtractReader htsExtractReader, IEmrMetricReader metricReader)
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

        public IEnumerable<IndicatorExtract> GenerateMigrations()
        {
            return _htsExtractReader.ReadAll();
        }
    }
}
