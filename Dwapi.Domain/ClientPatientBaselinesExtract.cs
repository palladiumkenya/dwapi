using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dwapi.Domain
{
    [Table("PatientBaselinesExtract")]
    public class ClientPatientBaselinesExtract : ClientExtract
    {
        [Key]
        public override Guid Id { get; set; }
        public int? bCD4 { get; set; }
        public DateTime? bCD4Date { get; set; }
        public int? bWAB { get; set; }
        public DateTime? bWABDate { get; set; }
        public int? bWHO { get; set; }
        public DateTime? bWHODate { get; set; }
        public int? eWAB { get; set; }
        public DateTime? eWABDate { get; set; }
        public int? eCD4 { get; set; }
        public DateTime? eCD4Date { get; set; }
        public int? eWHO { get; set; }
        public DateTime? eWHODate { get; set; }
        public int? lastWHO { get; set; }
        public DateTime? lastWHODate { get; set; }
        public int? lastCD4 { get; set; }
        public DateTime? lastCD4Date { get; set; }
        public int? lastWAB { get; set; }
        public DateTime? lastWABDate { get; set; }
        public int? m12CD4 { get; set; }
        public DateTime? m12CD4Date { get; set; }
        public int? m6CD4 { get; set; }
        public DateTime? m6CD4Date { get; set; }

        public ClientPatientBaselinesExtract()
        {
        }

        public ClientPatientBaselinesExtract(int patientPk, string patientId, int siteCode, int? bCd4, DateTime? bCd4Date, int? bWab, DateTime? bWabDate, int? bWho, DateTime? bWhoDate, int? eWab, DateTime? eWabDate, int? eCd4, DateTime? eCd4Date, int? eWho, DateTime? eWhoDate, int? lastWho, DateTime? lastWhoDate, int? lastCd4, DateTime? lastCd4Date, int? lastWab, DateTime? lastWabDate, int? m12Cd4, DateTime? m12Cd4Date, int? m6Cd4, DateTime? m6Cd4Date, string emr, string project)
        {
            PatientPK = patientPk;
            PatientID = patientId;
            SiteCode = siteCode;
            bCD4 = bCd4;
            bCD4Date = bCd4Date;
            bWAB = bWab;
            bWABDate = bWabDate;
            bWHO = bWho;
            bWHODate = bWhoDate;
            eWAB = eWab;
            eWABDate = eWabDate;
            eCD4 = eCd4;
            eCD4Date = eCd4Date;
            eWHO = eWho;
            eWHODate = eWhoDate;
            lastWHO = lastWho;
            lastWHODate = lastWhoDate;
            lastCD4 = lastCd4;
            lastCD4Date = lastCd4Date;
            lastWAB = lastWab;
            lastWABDate = lastWabDate;
            m12CD4 = m12Cd4;
            m12CD4Date = m12Cd4Date;
            m6CD4 = m6Cd4;
            m6CD4Date = m6Cd4Date;
            Emr = emr;
            Project = project;
        }

        public ClientPatientBaselinesExtract(TempPatientBaselinesExtract extract)
        {
            PatientPK = extract.PatientPK.Value;
            PatientID = extract.PatientID;
            SiteCode = extract.SiteCode.Value;
            bCD4 = extract.bCD4;
            bCD4Date = extract.bCD4Date;
            bWAB = extract.bWAB;
            bWABDate = extract.bWABDate;
            bWHO = extract.bWHO;
            bWHODate = extract.bWHODate;
            eWAB = extract.eWAB;
            eWABDate = extract.eWABDate;
            eCD4 = extract.eCD4;
            eCD4Date = extract.eCD4Date;
            eWHO = extract.eWHO;
            eWHODate = extract.eWHODate;
            lastWHO = extract.lastWHO;
            lastWHODate = extract.lastWHODate;
            lastCD4 = extract.lastCD4;
            lastCD4Date = extract.lastCD4Date;
            lastWAB = extract.lastWAB;
            lastWABDate = extract.lastWABDate;
            m12CD4 = extract.m12CD4;
            m12CD4Date = extract.m12CD4Date;
            m6CD4 = extract.m6CD4;
            m6CD4Date = extract.m6CD4Date;
            Emr = extract.Emr;
            Project = extract.Project;

        }
    }
}
