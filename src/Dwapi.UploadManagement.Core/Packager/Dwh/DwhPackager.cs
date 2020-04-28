using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Packager.Dwh
{
    public class DwhPackager : IDwhPackager
    {
        private readonly IDwhExtractReader _reader;
        private readonly IEmrMetricReader _metricReader;

        public DwhPackager(IDwhExtractReader reader, IEmrMetricReader metricReader)
        {
            _reader = reader;
            _metricReader = metricReader;
        }


        public IEnumerable<DwhManifest> Generate(EmrSetup emrSetup)
        {
            var sites = _reader.GetSites();
            var patientProfiles = _reader.GetSitePatientProfiles();
            return DwhManifest.Create(patientProfiles, emrSetup, sites);
        }

        public IEnumerable<DwhManifest> GenerateWithMetrics(EmrSetup emrSetup)
        {
            var metrics = _metricReader.ReadAll().FirstOrDefault();
            var appMetrics = _metricReader.ReadAppAll().ToList();
            var manifests = Generate(emrSetup).ToList();

            if (null != metrics)
            {
                foreach (var manifest in manifests)
                {
                    manifest.AddCargo(CargoType.Metrics, metrics);
                }
            }

            if (appMetrics.Any())
            {
                foreach (var manifest in manifests)
                {
                    foreach (var m in appMetrics)
                    {
                        manifest.AddCargo(CargoType.AppMetrics, m);
                    }
                }
            }

            return manifests;
        }

        public PatientExtractView GenerateExtracts(Guid id)
        {
            return _reader.Read(id);
        }
    }
}
