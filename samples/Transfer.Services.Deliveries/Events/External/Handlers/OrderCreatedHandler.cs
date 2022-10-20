using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transfer.CQRS.Events;
using Transfer.MessageBrokers;
using Transfer.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Threading;

namespace Transferor.Services.Deliveries.Events.External.Handlers
{
    public class OrderCreatedHandler : IEventHandler<OrderCreated>
    {
        private readonly IBusPublisher _publisher;
        private readonly IMessagePropertiesAccessor _messagePropertiesAccessor;
        private readonly ITracer _tracer;
        private readonly ILogger<OrderCreatedHandler> _logger;
        private readonly string _spanContextHeader;

        public OrderCreatedHandler(IBusPublisher publisher, IMessagePropertiesAccessor messagePropertiesAccessor,
            RabbitMqOptions rabbitMqOptions, ITracer tracer, ILogger<OrderCreatedHandler> logger)
        {
            _publisher = publisher;
            _messagePropertiesAccessor = messagePropertiesAccessor;
            _tracer = tracer;
            _logger = logger;
            _spanContextHeader = string.IsNullOrWhiteSpace(rabbitMqOptions.SpanContextHeader)
                ? "span_context"
                : rabbitMqOptions.SpanContextHeader;
        }

        public Task HandleAsync(OrderCreated @event, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Received 'order created' event with order id: {@event.OrderId}");
            var deliveryId = Guid.NewGuid();
            _logger.LogInformation($"Starting a delivery with id: {deliveryId}");

            //Refactor to an external decorator
            var correlationId = _messagePropertiesAccessor.MessageProperties.CorrelationId;
            var spanContext = string.Empty;
            if (_messagePropertiesAccessor.MessageProperties.Headers.TryGetValue(_spanContextHeader, out var span)
                && span is byte[] spanBytes)
            {
                spanContext = Encoding.UTF8.GetString(spanBytes);
            }

            if (string.IsNullOrWhiteSpace(spanContext))
            {
                spanContext = _tracer.ActiveSpan is null ? string.Empty : _tracer.ActiveSpan.Context.ToString();
            }

            return _publisher.PublishAsync(new DeliveryStarted(deliveryId), correlationId: correlationId,
                spanContext: spanContext);
        }
    }
}