using System.Threading.Tasks;

namespace Dwapi.SharedKernel.Events
{
    public interface IHandler<T> where T : IDomainEvent
    {
        Task Handle(T domainEvent);
    }
}
