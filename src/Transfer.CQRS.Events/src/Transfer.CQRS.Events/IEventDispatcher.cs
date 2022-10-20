using System.Threading;
using System.Threading.Tasks;

namespace Transfer.CQRS.Events
{
    public interface IEventDispatcher
    {
        Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    }
}