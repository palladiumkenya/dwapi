using Dwapi.SharedKernel.Events;

namespace Dwapi.UploadManagement.Core.Notifications.Dwh
{
    public class DwhMessageNotification : IDomainEvent
    {
        public bool Error { get; set; }
        public string Message { get; set; }

        public DwhMessageNotification(bool error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}