using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;

namespace Dwapi.ExtractsManagement.Core.Model.DTOs
{
    public class ExtractEventDTO
    {
        public string LastStatus { get; set; }
        public int? Found { get; set; }
        public int? Loaded { get; set; }
        public int? Rejected { get; set; }
        public int? Queued { get; set; }
        public int? Sent { get; set; }

        public ExtractEventDTO()
        {
        }

        public ExtractEventDTO(string lastStatus, int? found, int? loaded, int? sent)
        {
            LastStatus = lastStatus;
            Found = found;
            Loaded = loaded;
            //Rejected = Found - loaded;
            Sent = sent;
        }

        public static ExtractEventDTO Generate(List<ExtractHistory> extractHistories)
        {
            if(extractHistories.Count==0)
                return new ExtractEventDTO();

            var last = extractHistories.OrderByDescending(x => x.StatusDate).FirstOrDefault();

            var eventDTO = new ExtractEventDTO();

            if (null != last)
            {
                var lastStatus = $"{last.Status}";
                var found = GetStats(extractHistories, ExtractStatus.Found, ExtractStatus.Finding);
                var loaded = GetStats(extractHistories, ExtractStatus.Loaded, ExtractStatus.Loading);
                var sent = GetStats(extractHistories, ExtractStatus.Sent, ExtractStatus.Sending);
                return new ExtractEventDTO(lastStatus, found, loaded, sent);
            }

            return eventDTO;
        }

        private static int GetStats(List<ExtractHistory> extractHistories, ExtractStatus extractStatus,
            ExtractStatus extractStatusOther)
        {
            var history = extractHistories
                .Where(x => x.Status == extractStatus || x.Status == extractStatusOther)
                .OrderByDescending(x => x.StatusDate)
                .FirstOrDefault();

            return null != history && history.Stats.HasValue ? history.Stats.Value : 0;
        }

        public override string ToString()
        {
            return $@"{LastStatus} {nameof(Found)}:{Found} | {nameof(Loaded)}:{Loaded} | {nameof(Rejected)}:{Rejected}| {nameof(Queued)}:{Queued} | {nameof(Sent)}:{Sent}";
        }
    }
}