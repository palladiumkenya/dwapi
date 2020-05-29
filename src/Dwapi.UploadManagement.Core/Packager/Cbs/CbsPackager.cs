using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.UploadManagement.Core.Model;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

namespace Dwapi.UploadManagement.Core.Packager.Cbs
{
    public class CbsPackager: ICbsPackager
    {
        private readonly ICbsExtractReader _cbsExtractReader;
        private readonly IEmrMetricReader _metricReader;

        public CbsPackager(ICbsExtractReader cbsExtractReader, IEmrMetricReader metricReader)
        {
            _cbsExtractReader = cbsExtractReader;
            _metricReader = metricReader;
        }


        public IEnumerable<Manifest> Generate(EmrDto emrSetup)
        {
            var sites = _cbsExtractReader.GetSites();
            var profiles = _cbsExtractReader.GetSitePatientProfiles();

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

        public IEnumerable<MasterPatientIndexDto> GenerateDtoMpi()
        {
            var mpis = _cbsExtractReader.ReadAll().ToList();
            return Mapper.Map<List<MasterPatientIndexDto>>(mpis);
        }
    }
}
