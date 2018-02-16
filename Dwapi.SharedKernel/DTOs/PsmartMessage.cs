using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.SharedKernel.DTOs
{
    public class PsmartMessage
    {
    
        public List<string> Message { get; set; }=new List<string>();

        public PsmartMessage()
        {
        }

        public PsmartMessage(List<string> message)
        {
            Message = message;
        }

        public void AddMessage(string message)
        {
            Message.Add(message);
        }

        public void AddMessage(List<string> messages)
        {
            Message.AddRange(messages);
        }
    }
}