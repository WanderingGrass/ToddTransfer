using System;
using System.Threading.Tasks;
using Transfer.Persistence.OpenStack.OCS.Http;

namespace Transfer.Persistence.OpenStack.OCS.RequestHandler
{
    internal interface IRequestHandler
    {
        Task<HttpRequestResult> Send(Func<IHttpRequestBuilder, IHttpRequestBuilder> httpRequestBuilder);
    }
}