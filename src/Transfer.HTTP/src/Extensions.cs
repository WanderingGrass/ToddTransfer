using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace Transfer.HTTP
{
    public static class Extensions
    {
        private const string SectionName = "httpClient";
        private const string RegistryName = "http.client";

        public static ITransferBuilder AddHttpClient(this ITransferBuilder builder, string clientName = "Transfer",
            IEnumerable<string> maskedRequestUrlParts = null, string sectionName = SectionName,
            Action<IHttpClientBuilder> httpClientBuilder = null)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            if (string.IsNullOrWhiteSpace(clientName))
            {
                throw new ArgumentException("HTTP client name cannot be empty.", nameof(clientName));
            }

            var options = builder.GetOptions<HttpClientOptions>(sectionName);
            if (maskedRequestUrlParts is {} && options.RequestMasking is {})
            {
                options.RequestMasking.UrlParts = maskedRequestUrlParts;
            }

            builder.Services.AddSingleton<ICorrelationContextFactory, EmptyCorrelationContextFactory>();
            builder.Services.AddSingleton<ICorrelationIdFactory, EmptyCorrelationIdFactory>();
            builder.Services.AddSingleton(options);
            builder.Services.AddSingleton<IHttpClientSerializer, SystemTextJsonHttpClientSerializer>();
            var clientBuilder = builder.Services.AddHttpClient<IHttpClient, TransferHttpClient>(clientName);
            httpClientBuilder?.Invoke(clientBuilder);

            if (options.RequestMasking?.Enabled == true)
            {
                builder.Services.Replace(ServiceDescriptor
                    .Singleton<IHttpMessageHandlerBuilderFilter, TransferHttpLoggingFilter>());
            }

            return builder;
        }

        [Description("This is a hack related to HttpClient issue: https://github.com/aspnet/AspNetCore/issues/13346")]
        public static void RemoveHttpClient(this ITransferBuilder builder)
        {
            var registryType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .SingleOrDefault(t => t.Name == "HttpClientMappingRegistry");
            var registry = builder.Services.SingleOrDefault(s => s.ServiceType == registryType)?.ImplementationInstance;
            var registrations = registry?.GetType().GetProperty("TypedClientRegistrations");
            var clientRegistrations = registrations?.GetValue(registry) as IDictionary<Type, string>;
            clientRegistrations?.Remove(typeof(IHttpClient));
        }
    }
}