namespace Transfer.MessageBrokers.RabbitMQ
{
    public interface IRabbitMqPluginsRegistry
    {
        IRabbitMqPluginsRegistry Add<TPlugin>() where TPlugin : class, IRabbitMqPlugin;
    }
}