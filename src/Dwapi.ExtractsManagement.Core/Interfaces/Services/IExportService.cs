using System;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services
{
    public interface IExportService
    {
        Task<string> ExportToJSonAsync(string exportDir = "", IProgress<int> progress = null);
        string Base64Encode(string plainText);
        string Base64Decode(string base64EncodedData);
    }
}