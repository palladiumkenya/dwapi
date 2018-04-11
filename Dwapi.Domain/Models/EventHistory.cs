using Dwapi.Domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dwapi.Domain
{
    public class EventHistory
    {
        public Guid Id { get; set; }
        public int? SiteCode { get; set; }
        public string Display { get; set; }

        public int? Found { get; set; }
        public DateTime? FoundDate { get; set; }
        public string FoundStatus { get; set; }
        public bool? IsFoundSuccess { get; set; }

        public int? Loaded { get; set; }
        public int? Rejected { get; set; }
        public DateTime? LoadDate { get; set; }
        public string LoadStatus { get; set; }
        public bool? IsLoadSuccess { get; set; }

        public int? Imported { get; set; }
        public int? NotImported { get; set; }
        public DateTime? ImportDate { get; set; }
        public string ImportStatus { get; set; }
        public bool? IsImportSuccess { get; set; }


        public int? Sent { get; set; }
        public int? NotSent { get; set; }
        public DateTime? SendDate { get; set; }
        public string SendStatus { get; set; }
        public bool? IsSendSuccess { get; set; }

        public Guid ExtractSettingId { get; set; }

        [NotMapped]
        public string Emr { get; set; }
        [NotMapped]
        public string Project { get; set; }
        [NotMapped]
        public bool Processed { get; set; }
        [NotMapped]
        public bool Voided { get; set; }

        public EventHistory()
        {
        }

        private EventHistory(int? siteCode, string display, int? found, DateTime? foundDate, string foundStatus, bool? isFoundSuccess, Guid extractSettingId)
        {
            SiteCode = siteCode;
            Display = display;
            Found = found;
            FoundDate = foundDate;
            FoundStatus = foundStatus;
            IsFoundSuccess = isFoundSuccess;
            ExtractSettingId = extractSettingId;
        }


        public static EventHistory CreateFound(int? siteCode, string display, int? found, Guid extractSettingId)
        {
            return new EventHistory(siteCode, display, found, DateTime.Now, string.Empty, true, extractSettingId);
        }

        public string FoundInfo()
        {
            return $"{Display} > Found {Found} {FoundDate.GetTiming("|")}";
        }

        public string LoadInfo()
        {
            return $"{Display} > Loaded {Loaded}/{Found} {LoadDate.GetTiming("|")}";
        }

        public string ImportedInfo()
        {
            return $"{Display} > Imported {Imported}/{Loaded} {ImportDate.GetTiming("|")}";
        }

        public string NotImportedInfo()
        {
            return $"{Display} > Not Imported {NotImported}/{Imported} {ImportDate.GetTiming("|")}";
        }

        public string RejectedInfo()
        {
            return $"{Display} > Rejected {Rejected}/{Found} {LoadDate.GetTiming("|")}";
        }

        public string SendInfo()
        {
            return $"{Display} > Sent {Sent}/{Loaded} {SendDate.GetTiming("|")}";
        }
        public string NotSenTInfo()
        {
            return $"{Display} > Not Sent {NotSent}/{Sent} {SendDate.GetTiming("|")}";
        }
    }
}
