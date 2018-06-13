namespace Dwapi.SharedKernel.Model
{
    public class SendProgress
    {
        public string Extract { get; set; }
        public int Progress { get; set; }

        public SendProgress(string extract, int progress)
        {
            Extract = extract;
            Progress = progress;
        }
    }
}