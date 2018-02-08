using System.Net.Http;
using Dwapi.TransmissionManagement.Core.Interfaces.Services;
using Dwapi.TransmissionManagement.Core.Model;

namespace Dwapi.TransmissionManagement.Core.Services
{
    public class MessageSenderService: IMessageSenderService
    {
        private readonly HttpClient _client;

        public MessageSenderService(HttpClient client)
        {
            _client = client;
        }

        public void Send(string address, CardDetail cardDetail)
        {
          
        }
    }
}