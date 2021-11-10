using System;
using Transfer.CQRS.Events;

namespace Transferor.Services.Orders.Events
{
    public class OrderCreated : IEvent
    {
        public Guid OrderId { get; }

        public OrderCreated(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
