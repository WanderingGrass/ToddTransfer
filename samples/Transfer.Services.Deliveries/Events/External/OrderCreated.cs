using System;
using Transfer.CQRS.Events;
using Transfer.MessageBrokers;

namespace Transferor.Services.Deliveries.Events.External
{
    [Message("orders")]
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; }

        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
