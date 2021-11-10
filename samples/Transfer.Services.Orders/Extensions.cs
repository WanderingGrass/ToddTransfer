using Transfer;
using Transfer.WebApi.Security;
using Transferor.Services.Orders.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Transferor.Services.Orders
{
    public static class Extensions
    {
        public static ITransferBuilder AddServices(this ITransferBuilder builder)
        {
            builder.AddCertificateAuthentication();
            builder.Services.AddSingleton<IPricingServiceClient, PricingServiceClient>();
            return builder;
        }
    }
}
