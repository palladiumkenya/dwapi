using System;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.UploadManagement.Core.Exchange.Cbs
{
    public class SendMpiResponse
    {
        public string BatchKey { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(BatchKey);
        }
        public override string ToString()
        {
            return $"{BatchKey}";
        }
    }
}