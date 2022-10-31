using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Exchange;

namespace Dwapi.UploadManagement.Core.Interfaces.Packager.Hts
{
    public interface IHtsPackager
    {
        IEnumerable<Manifest> Generate(EmrDto emrDto);
        IEnumerable<Manifest> GenerateWithMetrics(EmrDto emrDto);
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
        IEnumerable<HtsEligibilityExtract> GenerateHtsEligibilityExtracts();
        IEnumerable<HtsRiskScores> GenerateHtsRiskScores();



    }
}