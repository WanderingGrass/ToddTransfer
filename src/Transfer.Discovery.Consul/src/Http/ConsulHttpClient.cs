using System.Net.Http;
using Transfer.HTTP;

namespace Transfer.Discovery.Consul.Http
{
    internal sealed class ConsulHttpClient : TransferHttpClient, IConsulHttpClient
    {
        public ConsulHttpClient(HttpClient client, HttpClientOptions options, IHttpClientSerializer serializer,
            ICorrelationContextFactory correlationContextFactory, ICorrelationIdFactory correlationIdFactory)
            : base(client, options, serializer, correlationContextFactory, correlationIdFactory)
        {
        }
    }
}