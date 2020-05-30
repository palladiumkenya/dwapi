using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;
using Dwapi.UploadManagement.Core.Model;

namespace Dwapi.UploadManagement.Core.Packager.Hts
{
    public class HtsPackager : IHtsPackager
    {
        private readonly IHtsExtractReader _htsExtractReader;
        private readonly IEmrMetricReader _metricReader;

        public HtsPackager(IHtsExtractReader htsExtractReader, IEmrMetricReader metricReader)
        {
            _htsExtractReader = htsExtractReader;
            _metricReader = metricReader;
        }


        public IEnumerable<Manifest> Generate(EmrDto emrSetup)
        {
            var sites = _htsExtractReader.GetSites();
            var profiles = _htsExtractReader.GetSitePatientProfiles();

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

        public IEnumerable<HtsClients> GenerateClients()
        {
            return _htsExtractReader.ReadAllClients();
        }

        public IEnumerable<HtsClientTests> GenerateClientTests()
        {
            return _htsExtractReader.ReadAllClientTests();
        }

        public IEnumerable<HtsClientTracing> GenerateClientTracing()
        {
            return _htsExtractReader.ReadAllClientTracing();
        }

        public IEnumerable<HtsPartnerTracing> GeneratePartnerTracing()
        {
            return _htsExtractReader.ReadAllPartnerTracing();
        }

        public IEnumerable<HtsPartnerNotificationServices> GeneratePartnerNotificationServices()
        {
            return _htsExtractReader.ReadAllPartnerNotificationServices();
        }

        public IEnumerable<HtsClientLinkage> GenerateClientLinkage()
        {
            return _htsExtractReader.ReadAllClientsLinkage();
        }

        public IEnumerable<HtsTestKits> GenerateTestKits()
        {
            return _htsExtractReader.ReadAllTestKits();
        }
    }
}
