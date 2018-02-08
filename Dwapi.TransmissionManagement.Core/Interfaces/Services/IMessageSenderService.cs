using Dwapi.TransmissionManagement.Core.Model;

namespace Dwapi.TransmissionManagement.Core.Interfaces.Services
{
    public interface IMessageSenderService
    {
        void Send(string address,CardDetail cardDetail);
    }
}