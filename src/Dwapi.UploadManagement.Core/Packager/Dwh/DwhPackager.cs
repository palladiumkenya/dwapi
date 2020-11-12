using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
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
        private readonly IDiffLogReader _diffLogReader;

        public DwhPackager(IDwhExtractReader reader, IEmrMetricReader metricReader, IDiffLogReader diffLogReader)
        {
            _reader = reader;
            _metricReader = metricReader;
            _diffLogReader = diffLogReader;
        }


        public IEnumerable<DwhManifest> Generate(EmrDto emrSetup)
        {
            var sites = _reader.GetSites();
            var patientProfiles = _reader.GetSitePatientProfiles();
            return DwhManifest.Create(patientProfiles, emrSetup, sites);
        }

        public IEnumerable<DwhManifest> GenerateWithMetrics(EmrDto emrSetup)
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

        public IEnumerable<DwhManifest> GenerateDiffWithMetrics(EmrDto emrSetup)
        {
            var manifests = GenerateWithMetrics(emrSetup).ToList();

            foreach (var dwhManifest in manifests)
            {
                dwhManifest.UploadMode = UploadMode.Differential;
            }

            return manifests;
        }

        public PatientExtractView GenerateExtracts(Guid id)
        {
            return _reader.Read(id);
        }

        public IEnumerable<T> GenerateBatchExtracts<T, TId>(int page, int batchSize) where T : Entity<TId>
        {
            return _reader.Read<T, TId>(page, batchSize);
        }

        public IEnumerable<T> GenerateBatchExtracts<T>(int page, int batchSize) where T : ClientExtract
        {
            return GenerateBatchExtracts<T,Guid>(page,batchSize);
        }

        public IEnumerable<T> GenerateDiffBatchExtracts<T>(int page, int batchSize,string docket,string extract) where T : ClientExtract
        {
            var allDifflogs = _diffLogReader.ReadAll().ToList();

            var diffLog = allDifflogs.FirstOrDefault(x =>
                x.Docket.ToLower() == docket.ToLower() && x.Extract.ToLower() == extract.ToLower());

            if (null == diffLog)
                return GenerateBatchExtracts<T, Guid>(page, batchSize);

            if(diffLog.LastCreated.IsNullOrEmpty() && diffLog.LastCreated.IsNullOrEmpty())
                return GenerateBatchExtracts<T, Guid>(page, batchSize);

            if(diffLog.LastModified.IsNullOrEmpty() && !diffLog.LastCreated.IsNullOrEmpty())
                return _reader.Read<T, Guid>(page, batchSize, x => x.Date_Created > diffLog.LastCreated);

            if(!diffLog.LastModified.IsNullOrEmpty() && diffLog.LastCreated.IsNullOrEmpty())
                return _reader.Read<T, Guid>(page, batchSize, x => x.Date_Created > diffLog.LastModified);

            return _reader.Read<T, Guid>(page, batchSize,
                x => x.Date_Created > diffLog.LastCreated || x.Date_Last_Modified > diffLog.LastModified);
        }

        public IEnumerable<T> GenerateDiffBatchMainExtracts<T>(int page, int batchSize, string docket, string extract) where T : ClientExtract
        {
            var allDifflogs = _diffLogReader.ReadAll().ToList();

            var diffLog = allDifflogs.FirstOrDefault(x =>
                x.Docket.ToLower() == docket.ToLower() && x.Extract.ToLower() == extract.ToLower());

            if (null == diffLog)
                return new List<T>();

            if(diffLog.LastCreated.IsNullOrEmpty() && diffLog.LastCreated.IsNullOrEmpty())
                return new List<T>();

            if(diffLog.LastModified.IsNullOrEmpty() && !diffLog.LastCreated.IsNullOrEmpty())
                return _reader.ReadMainExtract<T, Guid>(page, batchSize, x => x.Date_Created > diffLog.LastCreated);

            if(!diffLog.LastModified.IsNullOrEmpty() && diffLog.LastCreated.IsNullOrEmpty())
                return _reader.ReadMainExtract<T, Guid>(page, batchSize, x => x.Date_Created > diffLog.LastModified);

            return _reader.ReadMainExtract<T, Guid>(page, batchSize,
                x => x.Date_Created > diffLog.LastCreated || x.Date_Last_Modified > diffLog.LastModified);
        }

        public PackageInfo GetPackageInfo<T, TId>(int batchSize) where T : Entity<TId>
        {

            var count=_reader.GetTotalRecords<T,TId>();
            var pageCount=new PackagePager().PageCount(batchSize,count);


            return new PackageInfo(pageCount,count,batchSize);
        }

        public PackageInfo GetPackageInfo<T>(int batchSize) where T : ClientExtract
        {
            return GetPackageInfo<T, Guid>(batchSize);
        }
    }
}
