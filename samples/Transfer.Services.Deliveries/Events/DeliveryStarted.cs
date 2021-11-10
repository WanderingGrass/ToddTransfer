using System;
using Transfer.CQRS.Events;

namespace Transferor.Services.Deliveries.Events
{
    public class DeliveryStarted : IEvent
    {
        public Guid DeliveryId { get; }

        public DeliveryStarted(Guid deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}
