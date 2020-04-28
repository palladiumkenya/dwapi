using System;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class Extract : DbExtract
    {
        public string Destination { get; set; }
        public Guid EmrSystemId { get; set; }
        public string DocketId { get; set; }
        public Guid? DatabaseProtocolId { get; set; }

        public static Extract CreatePsmart(Guid emrSystemId, string docketId = "PSMART")
        {
            var extract = new Extract();
            extract.EmrSystemId = emrSystemId;
            extract.DocketId = docketId;
            extract.Name = "pSmart";
            extract.Display = "Smart Card";
            extract.Destination = "PSmartStage";
            extract.ExtractSql = "select id,shr,date_created,status,status_date,uuid FROM psmart_store where upper(status) = 'PENDING'";
            return extract;
        }
    }
}