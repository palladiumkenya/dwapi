using System;
using System.Collections.Generic;
using Dwapi.Contracts.Exchange;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Humanizer;

namespace Dwapi.UploadManagement.Core.Interfaces.Exchange
{
    public interface IMessageSourceBag<T>:ISourceBag<T>
        where T:ClientExtract
    {
        int Stake { get; }
        string EndPoint { get; }
        string ExtractName { get; }
        ExtractType ExtractType { get; }
        string Docket { get; }
        string DocketExtract { get; }
        List<Guid> SendIds { get; }
        int MinPk { get; }
        int MaxPk { get; }
        void Generate(List<T> extracts,Guid manifestId,Guid facilityId,string jobId);
        int GetProgress(int count, int total);
    }
}
