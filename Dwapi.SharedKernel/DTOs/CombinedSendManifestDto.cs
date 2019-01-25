namespace Dwapi.SharedKernel.DTOs
{
    public class CombinedSendManifestDto
    {
        public SendManifestPackageDTO DwhPackage { get; set; }
        public SendManifestPackageDTO MpiPackage { get; set; }
        public bool SendMpi { get; set; }
    }
}