namespace Dwapi.SharedKernel.Model
{
    public class DwhProgress
    {
        public string Extract { get; set; }
        public string Status { get; set; }
        public int Found { get; set; }
        public int Loaded { get; set; }
        public int Rejected { get; set; }
        public int Queued { get; set; }
        public int Sent { get; set; }

        public DwhProgress(string extract, string status)
        {
            Extract = extract;
            Status = status;
        }

        public DwhProgress(string extract, string status, int found,int loaded,int rejected,int queued,int sent):this(extract,status)
        {
            Found = found;
            Loaded = loaded;
            Rejected = rejected;
            Queued = queued;
            Sent = sent;
        }
    }
}
