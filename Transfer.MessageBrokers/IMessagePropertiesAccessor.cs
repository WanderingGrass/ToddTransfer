namespace Transfer.MessageBrokers
{
    public interface IMessagePropertiesAccessor
    {
        IMessageProperties MessageProperties { get; set; }
    }
}