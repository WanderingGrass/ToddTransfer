using System;
using Transfer.CQRS.Queries;
using Transferor.Services.Orders.DTO;

namespace Transferor.Services.Orders.Queries
{
    public class GetOrder : IQuery<OrderDto>
    {
        public Guid OrderId { get; set; }
    }
}
