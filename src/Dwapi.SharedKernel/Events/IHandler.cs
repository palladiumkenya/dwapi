namespace Dwapi.SharedKernel.Events
{
    public interface IHandler<T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}