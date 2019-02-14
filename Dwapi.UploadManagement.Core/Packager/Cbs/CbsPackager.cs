using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Interfaces.Packager.Cbs;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Cbs;

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


        public IEnumerable<Manifest>  Generate()
        {
            var getPks = _cbsExtractReader.ReadAll()
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

        public IEnumerable<MasterPatientIndex> GenerateMpi()
        {
            return _cbsExtractReader.ReadAll();
        }
    }
}
