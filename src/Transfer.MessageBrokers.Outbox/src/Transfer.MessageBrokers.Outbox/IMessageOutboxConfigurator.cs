namespace Transfer.MessageBrokers.Outbox
{
    public interface IMessageOutboxConfigurator
    {
        ITransferBuilder Builder { get; }
        OutboxOptions Options { get; }
    }
}