using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Interfaces.Services
{
    public interface IManifestService
    {
        Manifest Generate();
    }
}