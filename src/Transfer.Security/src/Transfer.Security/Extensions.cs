using Transfer.Security.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace Transfer.Security
{
    public static class Extensions
    {
        public static ITransferBuilder AddSecurity(this ITransferBuilder builder)
        {
            builder.Services
                .AddSingleton<IEncryptor, Encryptor>()
                .AddSingleton<IHasher, Hasher>()
                .AddSingleton<ISigner, Signer>();

            return builder;
        }
    }
}