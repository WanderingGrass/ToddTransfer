using System.Threading.Tasks;

namespace Transfer.CQRS.Events
{
    public interface IEventDispatcher
    {
        Task PublishAsync<T>(T @event) where T : class, IEvent;
    }
}