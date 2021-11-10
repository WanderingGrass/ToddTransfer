namespace Transfer.MessageBrokers.Outbox.Configurators
{
    internal sealed class MessageOutboxConfigurator : IMessageOutboxConfigurator
    {
        public ITransferBuilder Builder { get; }
        public OutboxOptions Options { get; }

        public MessageOutboxConfigurator(ITransferBuilder builder, OutboxOptions options)
        {
            Builder = builder;
            Options = options;
        }
    }
}