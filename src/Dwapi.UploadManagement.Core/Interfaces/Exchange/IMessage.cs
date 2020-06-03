using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Interfaces.Exchange
{
    public interface IMessage<T> where T : ClientExtract
    {
        Facility Facility { get; }
        PatientExtractView Demographic { get; }
        List<T> Extracts { get; }
        List<Guid> SendIds { get; }
        IMessage<T> Generate(T extract);
        List<IMessage<T>> GenerateMessages(List<T> extracts);
        bool HasContents { get; }
    }
}
