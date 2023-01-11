using Dwapi.SharedKernel.Events;

namespace Dwapi.UploadManagement.Core.Notifications.Dwh
{
    public class DwExporthMessageNotification : IDomainEvent
    {
        public bool Error { get; set; }
        public string Message { get; set; }

        public DwExporthMessageNotification(bool error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}