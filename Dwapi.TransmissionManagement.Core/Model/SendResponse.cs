using System;

namespace Dwapi.TransmissionManagement.Core.Model
{
    public class SendResponse
    {
        public Guid MessageId { get; set; }
        public string Status { get; set; }
        public bool IsSuccess { get; }
        public DateTime DateSent { get; set; }

    }
}