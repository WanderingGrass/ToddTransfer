using System.Net.Http;
using Transfer.HTTP;

namespace Transfer.LoadBalancing.Fabio.Http
{
    internal sealed class FabioHttpClient : TransferHttpClient, IFabioHttpClient
    {
        public FabioHttpClient(HttpClient client, HttpClientOptions options, IHttpClientSerializer serializer,
            ICorrelationContextFactory correlationContextFactory, ICorrelationIdFactory correlationIdFactory)
            : base(client, options, serializer, correlationContextFactory, correlationIdFactory)
        {
        }
    }
}