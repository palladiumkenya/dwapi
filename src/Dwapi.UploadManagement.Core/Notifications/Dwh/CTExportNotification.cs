using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.UploadManagement.Core.Notifications.Dwh
{
    public class CTExportNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public CTExportNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
