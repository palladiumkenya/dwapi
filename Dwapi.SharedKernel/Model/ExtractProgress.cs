namespace Dwapi.SharedKernel.Model
{
    public class ExtractProgress: DwhProgress
    {
        public ExtractProgress(string extract, string status) : base(extract, status)
        {
        }

        public ExtractProgress(string extract, string status, int found, int loaded, int rejected, int queued, int sent) : base(extract, status, found, loaded, rejected, queued, sent)
        {
        }
    }
}
