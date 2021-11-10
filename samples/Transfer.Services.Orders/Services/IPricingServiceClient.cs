using System;
using System.Threading.Tasks;
using Transferor.Services.Orders.DTO;

namespace Transferor.Services.Orders.Services
{
    public interface IPricingServiceClient
    {
        Task<PricingDto> GetAsync(Guid orderId);
    }
}
