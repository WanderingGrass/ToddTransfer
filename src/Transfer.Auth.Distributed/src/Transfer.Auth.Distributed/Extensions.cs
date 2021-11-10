using Microsoft.Extensions.DependencyInjection;

namespace Transfer.Auth.Distributed
{
    public static class Extensions
    {
        private const string RegistryName = "auth.distributed";

        public static ITransferBuilder AddDistributedAccessTokenValidator(this ITransferBuilder builder)
        {
            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            builder.Services.AddSingleton<IAccessTokenService, DistributedAccessTokenService>();

            return builder;
        }
    }
}