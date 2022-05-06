using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Dwh
{
    public interface IDwhPackager
    {
        IEnumerable<DwhManifest> Generate(EmrDto emrDto);
        IEnumerable<DwhManifest> GenerateWithMetrics(EmrDto emrDto);
        IEnumerable<DwhManifest> GenerateDiffWithMetrics(EmrDto emrDto);
        PatientExtractView GenerateExtracts(Guid id);

        IEnumerable<T>  GenerateBatchExtracts<T, TId>(int page,int batchSize) where T : Entity<TId>;
        IEnumerable<T>  GenerateSmartBatchExtracts<T, TId>(int page,int batchSize) where T : Entity<TId>;
        IEnumerable<T>  GenerateBatchExtracts<T>(int page,int batchSize) where T :ClientExtract;
        IEnumerable<T>  GenerateSmartBatchExtracts<T>(int page,int batchSize) where T :ClientExtract;

        IEnumerable<T> GenerateDiffBatchExtracts<T>(int page, int batchSize, string docket, string extract)
            where T : ClientExtract;

        IEnumerable<T> GenerateDiffBatchMainExtracts<T>(int page, int batchSize, string docket, string extract)
            where T : ClientExtract;

        PackageInfo GetPackageInfo<T, TId>(int batchSize) where T : Entity<TId>;
        PackageInfo GetPackageInfo<T>(int batchSize) where T : ClientExtract;
    }
}
