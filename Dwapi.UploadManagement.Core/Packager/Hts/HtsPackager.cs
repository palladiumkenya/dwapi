using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Hts;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;

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


        public IEnumerable<Manifest>  Generate()
        {
            var getPks = _htsExtractReader.ReadAllClients()
                .Select(x => new SitePatientProfile(x.SiteCode, x.FacilityName, x.PatientPk));


            return Manifest.Create(getPks);
        }

        public IEnumerable<Manifest> GenerateWithMetrics()
        {
            var metrics = _metricReader.ReadAll().FirstOrDefault();
            var manifests = Generate().ToList();

            if (null != metrics)
            {
                foreach (var manifest in manifests)
                {
                    manifest.AddCargo(metrics);
                }
            }

            return manifests;
        }

        public IEnumerable<HTSClientExtract> GenerateClients()
        {
            return _htsExtractReader.ReadAllClients();
        }

        public IEnumerable<HTSClientPartnerExtract> GeneratePartners()
        {
            return _htsExtractReader.ReadAllPartners();
        }

        public IEnumerable<HTSClientLinkageExtract> GenerateLinkages()
        {
            return _htsExtractReader.ReadAllLinkages();
        }
    }
}
