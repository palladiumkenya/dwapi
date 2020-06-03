namespace Dwapi.SharedKernel.Model
{
    public class SendProgress
    {
        public string Extract { get; set; }
        public int Progress { get; set; }
        public bool Done { get; set; }

        public SendProgress(string extract, int progress,bool done=false)
        {
            Extract = extract;
            Progress = progress;
            Done = done;
        }
    }
}
