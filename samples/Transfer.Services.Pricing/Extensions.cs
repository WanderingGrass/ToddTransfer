using Transfer;
using Transfer.WebApi.Security;

namespace Transferor.Services.Pricing
{
    public static class Extensions
    {
        public static ITransferBuilder AddServices(this ITransferBuilder builder)
        {
            builder.AddCertificateAuthentication();
            return builder;
        }
    }
}
