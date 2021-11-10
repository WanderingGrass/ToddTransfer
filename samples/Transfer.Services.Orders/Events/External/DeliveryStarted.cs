using System;
using Transfer.CQRS.Events;
using Transfer.MessageBrokers;

namespace Transferor.Services.Orders.Events.External
{
    [Message("deliveries")]
    public class DeliveryStarted : IEvent
    {
        public Guid DeliveryId { get; }

        public DeliveryStarted(Guid deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}
