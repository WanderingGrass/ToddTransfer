using System;

namespace Transferor.Services.Orders.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
