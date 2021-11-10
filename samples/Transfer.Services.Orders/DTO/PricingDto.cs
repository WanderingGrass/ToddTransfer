using System;

namespace Transferor.Services.Orders.DTO
{
    public class PricingDto
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
