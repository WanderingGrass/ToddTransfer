namespace Transfer.MessageBrokers
{
    public interface ICorrelationContextAccessor
    {
        object CorrelationContext { get; set; }
    }
}