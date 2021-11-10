using System.Threading.Tasks;

namespace Transfer.MessageBrokers.RawRabbit
{
    public interface IMessageProcessor
    {
        Task<bool> TryProcessAsync(string id);
        Task RemoveAsync(string id);
    }
}