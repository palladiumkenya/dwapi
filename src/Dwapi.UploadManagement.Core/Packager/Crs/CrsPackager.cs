using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Crs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Crs;
using Dwapi.UploadManagement.Core.Model;
using Dwapi.UploadManagement.Core.Model.Crs;
using Dwapi.UploadManagement.Core.Model.Crs.Dtos;
using Dwapi.UploadManagement.Core.Profiles;

namespace Dwapi.UploadManagement.Core.Packager.Crs
{
    public class CrsPackager: ICrsPackager
    {
        private readonly ICrsExtractReader _crsExtractReader;
        private readonly IEmrMetricReader _metricReader;

        public CrsPackager(ICrsExtractReader crsExtractReader, IEmrMetricReader metricReader)
        {
            _crsExtractReader = crsExtractReader;
            _metricReader = metricReader;
        }


        public IEnumerable<Manifest> Generate(EmrDto emrSetup)
        {
            var sites = _crsExtractReader.GetSites();
            var profiles = _crsExtractReader.GetSitePatientProfiles();

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

        public IEnumerable<ClientRegistryExtractView> GenerateCrs()
        {
            var crss = _crsExtractReader.ReadAll().ToList();
            return crss;
        }
    }
}
