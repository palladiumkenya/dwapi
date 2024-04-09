﻿namespace Dwapi.SharedKernel.DTOs
{
    public class CombinedDataReaderSendManifestDto
    {
        public SendManifestPackageDTO DwhPackage { get; set; }
        public SendManifestPackageDTO MpiPackage { get; set; }
        
        public bool SendMpi { get; set; }
        public string JobId { get; set; }
        public string dbProtocol { get; set; }

        public bool IsValid()
        {
            return (null != DwhPackage && DwhPackage.IsValid());
        }
    }
}
