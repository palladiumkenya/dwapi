namespace Dwapi.SharedKernel.Model
{
    public class SendProgress
    {
        public string Extract { get; set; }
        public int Progress { get; set; }
        public bool Done { get; set; }
        public long Sent { get; set; }

        public SendProgress(string extract, int progress, long sent, bool done=false)
        {
            Extract = extract;
            Progress = progress;
            Sent = sent;
            Done = done;
        }
    }
}
