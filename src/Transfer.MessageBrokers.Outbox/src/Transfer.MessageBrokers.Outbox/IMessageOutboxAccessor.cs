using System.Collections.Generic;
using System.Threading.Tasks;
using Transfer.MessageBrokers.Outbox.Messages;

namespace Transfer.MessageBrokers.Outbox
{
    public interface IMessageOutboxAccessor
    {
        Task<IReadOnlyList<OutboxMessage>> GetUnsentAsync();
        Task ProcessAsync(OutboxMessage message);
        Task ProcessAsync(IEnumerable<OutboxMessage> outboxMessages);
    }
}