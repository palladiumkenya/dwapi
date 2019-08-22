using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Exchange;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Core.Model.Hts;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Hts
{
    public interface IHtsPackager
    {
        IEnumerable<Manifest> Generate();
        IEnumerable<Manifest> GenerateWithMetrics();
        //IEnumerable<HTSClientExtract> GenerateClients();
        //IEnumerable<HTSClientPartnerExtract> GeneratePartners();
        //IEnumerable<HTSClientLinkageExtract> GenerateLinkages();
        IEnumerable<HtsClients> GenerateClients();
        IEnumerable<HtsClientTests> GenerateClientTests();
        IEnumerable<HtsTestKits> GenerateTestKits();
        IEnumerable<HtsClientTracing> GenerateClientTracing();
        IEnumerable<HtsPartnerTracing> GeneratePartnerTracing();
        IEnumerable<HtsPartnerNotificationServices> GeneratePartnerNotificationServices();
        IEnumerable<HtsClientLinkage> GenerateClientLinkage();

    }
}
