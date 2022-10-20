using System.Threading;
using System.Threading.Tasks;

namespace Transfer.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    }
}