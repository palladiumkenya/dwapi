using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Humanizer;

namespace Dwapi.UploadManagement.Core.Interfaces.Exchange
{
    public interface IMessageBag<T> where T:ClientExtract
    {
        string EndPoint { get; }
        IMessage<T> Message { get; set; }
        List<IMessage<T>> Messages { get; set; }
        List<Guid> SendIds { get; }
        IMessageBag<T> Generate(List<T> extracts);
        string ExtractName { get; }
        ExtractType ExtractType { get; }
        string Docket { get; }
        int GetProgress(int count, int total);
    }
}
