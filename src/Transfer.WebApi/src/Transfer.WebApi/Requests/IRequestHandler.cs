using System.Threading;
using System.Threading.Tasks;

namespace Transfer.WebApi.Requests
{
    public interface IRequestHandler<in TRequest, TResult> where TRequest : class, IRequest 
    {
        Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}