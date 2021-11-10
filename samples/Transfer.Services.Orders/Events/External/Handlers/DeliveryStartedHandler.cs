using System.Threading.Tasks;
using Transfer.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace Transferor.Services.Orders.Events.External.Handlers
{
    public class DeliveryStartedHandler : IEventHandler<DeliveryStarted>
    {
        private readonly ILogger<DeliveryStartedHandler> _logger;

        public DeliveryStartedHandler(ILogger<DeliveryStartedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(DeliveryStarted @event)
        {
            _logger.LogInformation($"Received 'delivery started' event with delivery id: {@event.DeliveryId}");
            return Task.CompletedTask;
        }
    }
}
